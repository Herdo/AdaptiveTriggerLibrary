namespace AdaptiveTriggerLibrary.Functional
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices.WindowsRuntime;

    /// <summary>
    /// A base class that can monitor any event in a generic way.
    /// </summary>
    internal class EventWatcher : IDisposable
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private object _source;
        private EventInfo _eventInfo;
        private Delegate _handler;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="EventWatcher"/> instance.
        /// </summary>
        /// <param name="source">The source object that will be raising the event.</param>
        /// <param name="eventName">The name of the event to subscribe to.</param>
        public EventWatcher(object source, string eventName)
        {
            Subscribe(source, eventName);
        }

        #endregion
        
        ///////////////////////////////////////////////////////////////////
        #region Protected Methods

        /// <summary>
        /// Called when the event being watched is raised.
        /// </summary>
        /// <param name="parameters">The parameters of the event.</param>
        protected virtual void OnEventRaised(params object[] parameters)
        {

        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        /// <summary>
        /// Dynamically creates a handler for any event.
        /// </summary>
        /// <param name="signature">The <see cref="Type"/> that represents the event (or delegate) signature.</param>
        private void CreateHandlerDelegate(Type signature)
        {
            // Get the event delegate's parameters from its 'Invoke' method.
            var invokeMethod = signature.GetTypeInfo().GetDeclaredMethod("Invoke");

            // Determine what parameters are passed into the handler
            var parameters = invokeMethod.GetParameters()
                .Select((p) => Expression.Parameter(p.ParameterType, p.Name)).ToArray();

            // Create an expression to convert the individual parameters into a param array
            var paramArray = Expression.NewArrayInit(typeof(object), parameters);

            // Get a reference to the EventRaised method
            var eventRaisedMethod = typeof(EventWatcher).GetTypeInfo().GetDeclaredMethod(nameof(OnEventRaised));

            // Create an expression that calls the EventRaised method passing the parameter array
            var body = Expression.Call(Expression.Constant(this), eventRaisedMethod, paramArray);

            // Create a lambda from the body and parameters
            var listener = Expression.Lambda(signature, body, parameters);

            // Compile the lambda, converting it to a delegate
            _handler = listener.Compile();
        }

        /// <summary>
        /// Subscribes to the named event on the specified object instance.
        /// </summary>
        /// <param name="source">The source object that will be raising the event.</param>
        /// <param name="eventName">The name of the event to subscribe to.</param>
        private void Subscribe(object source, string eventName)
        {
            // Validate
            if (source == null) throw new ArgumentNullException("target");
            if (string.IsNullOrEmpty(eventName)) throw new ArgumentException("eventName");

            // Store
            this._source = source;

            // Attempt to get the runtime event
            _eventInfo = source.GetType().GetRuntimeEvent(eventName);

            // Make sure event was found
            if (_eventInfo == null)
            {
                throw new InvalidOperationException(string.Format("RuntimeEvent '{0}' was not found on type '{1}'", eventName, source.GetType().Name));
            }

            // Get pointers to the add and remove methods
            MethodInfo addMethod = _eventInfo.AddMethod;
            MethodInfo removeMethod = _eventInfo.RemoveMethod;

            // Get the parameter list from the add method
            ParameterInfo[] addParameters = addMethod.GetParameters();

            // The method signature for the event handler is the first parameter
            Type delegateType = addParameters[0].ParameterType;

            // Create a generic handler that matches the signature and forwards calls to our EventRaised method
            CreateHandlerDelegate(delegateType);

            // Use the RuntimeMarshaler to subscribe to the event
            Func<object, EventRegistrationToken> add = a => (EventRegistrationToken)addMethod.Invoke(source, new object[] { _handler });
            Action<EventRegistrationToken> remove = t => removeMethod.Invoke(source, new object[] { t });
            WindowsRuntimeMarshal.AddEventHandler(add, remove, _handler);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            // Remove the event handler
            if (_handler != null)
            {
                _eventInfo.RemoveEventHandler(_source, _handler);
                _handler = null;
            }
        }

        #endregion
    }

    /// <summary>
    /// Allows multiple subscriptions to an event without pinning subscribers in memory.
    /// </summary>
    internal class WeakEvent : EventWatcher
    {
        ///////////////////////////////////////////////////////////////////
        #region Static Version

        ///////////////////////////////////////////////////////////////////
        #region Fields

        static private readonly Collection<WeakEvent> Registrations = new Collection<WeakEvent>();

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        static private WeakEvent FindRegistration(object source, string eventName)
        {
            // Look for registration
            WeakEvent reg = null;
            for (var iReg = Registrations.Count - 1; iReg >= 0; iReg--)
            {
                // Get the registration
                reg = Registrations[iReg];

                // If the source is dead, remove the registration
                if ((reg != null) && (!reg._source.IsAlive))
                {
                    Registrations.RemoveAt(iReg);
                    reg = null;
                }

                // If it's the right source, done searching
                if ((reg._source.Target == source) && (reg._eventName == eventName))
                {
                    break;
                }
                reg = null;
            }

            // Done searching
            return reg;
        }

        static private void VerifyDelegate<THandler>() where THandler : class
        {
            // Verify handler type
            if (!typeof (Delegate).GetTypeInfo().IsAssignableFrom(typeof (THandler).GetTypeInfo()))
                throw new InvalidOperationException(
                    string.Format("The type '{0}' is not a delegate and cannot be used for event subscriptions.",
                        typeof (THandler).Name));
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Public Methods

        /// <summary>
        /// Adds a weak subscription to the specified handler.
        /// </summary>
        /// <param name="source">The object that is the source of the event.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="handler">The handler to subscribe.</param>
        static public void Subscribe<THandler>(object source, string eventName, THandler handler) where THandler : class
        {
            // Verify delegate type
            VerifyDelegate<THandler>();

            // Try to find existing
            var reg = FindRegistration(source, eventName);

            // If not found, create one and store it
            if (reg == null)
            {
                reg = new WeakEvent(source, eventName);
                Registrations.Add(reg);
            }

            // Add the subscription
            reg.Subscribe(handler as Delegate);
        }

        /// <summary>
        /// Removes a subscription to the specified handler.
        /// </summary>
        /// <param name="source">The object that is the source of the event.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="handler">The handler to unsubscribe.</param>
        static public void Unsubscribe<THandler>(object source, string eventName, THandler handler) where THandler : class
        {
            // Verify delegate type
            VerifyDelegate<THandler>();

            // Try to find existing
            var reg = FindRegistration(source, eventName);

            // If found, unsubscribe
            reg?.Unsubscribe(handler as Delegate);
        }
        #endregion

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Instance Version

        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly string _eventName;
        private readonly WeakReference _source;
        private readonly SubscriberCollection _subscriptions = new SubscriberCollection();

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="WeakEvent"/> instance.
        /// </summary>
        /// <param name="source">The source object that will be raising the event.</param>
        /// <param name="eventName">The name of the event to subscribe to.</param>
        private WeakEvent(object source, string eventName) : base(source, eventName)
        {
            // Store
            _source = new WeakReference(source);
            _eventName = eventName;
        }

        #endregion
        
        ///////////////////////////////////////////////////////////////////
        #region Base Overrides

        protected override void OnEventRaised(params object[] parameters)
        {
            // Pass to base first
            base.OnEventRaised(parameters);

            // Do for all subscribers
            for (var iSub = _subscriptions.Count - 1; iSub >= 0; iSub--)
            {
                // Get the subscription
                var sub = _subscriptions[iSub];

                // Alive?
                if (sub.Instance.IsAlive)
                {
                    // Call the subscriber safely
                    sub.Method.Invoke(sub.Instance.Target, parameters);
                }
                else
                {
                    // Reference dead. Might as well remove it.
                    _subscriptions.RemoveAt(iSub);
                }
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private WeakSubscription FindSubscription(Delegate handler)
        {
            // Get the target
            var target = handler.Target;

            // Get the new info
            var method = handler.GetMethodInfo();

            // Make sure it's not already subscribed
            for (var iSub = _subscriptions.Count - 1; iSub >= 0; iSub--)
            {
                // Get the subscription
                var sub = _subscriptions[iSub];

                // Subscriber still alive?
                if (sub.Instance.IsAlive)
                {
                    // Already subscribed?
                    if ((sub.Instance.Target == target) && (sub.Method == method)) { return sub; }
                }
                else
                {
                    // Reference dead. Might as well remove it.
                    _subscriptions.RemoveAt(iSub);
                }
            }

            // Not found
            return null;
        }

        /// <summary>
        /// Adds a weak subscription to the specified handler.
        /// </summary>
        /// <param name="handler">The handler to subscribe.</param>
        private void Subscribe(Delegate handler)
        {
            // Try to find existing subscription
            var existing = FindSubscription(handler);

            // If not subscribed yet, subscribe
            if (existing == null)
                _subscriptions.Add(new WeakSubscription(handler));
        }

        /// <summary>
        /// Removes a subscription to the specified handler.
        /// </summary>
        /// <param name="handler">
        /// The handler to unsubscribe.
        /// </param>
        private void Unsubscribe(Delegate handler)
        {
            // Try to find existing subscription
            var existing = FindSubscription(handler);

            // If found, remove it
            if (existing != null)
                _subscriptions.Remove(existing);
        }

        #endregion

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Types

        private class WeakSubscription
        {
            public WeakReference Instance { get; }
            public MethodInfo Method { get; }

            public WeakSubscription(Delegate del)
            {
                Instance = new WeakReference(del.Target);
                Method = del.GetMethodInfo();
            }
        }

        private class SubscriberCollection : Collection<WeakSubscription> { }

        #endregion
    }
}
