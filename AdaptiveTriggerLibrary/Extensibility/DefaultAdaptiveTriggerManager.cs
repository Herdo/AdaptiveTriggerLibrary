namespace AdaptiveTriggerLibrary.Extensibility
{
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using Triggers;

    /// <summary>
    /// A default implementation for the <see cref="IAdaptiveTriggerManager"/> interface.
    /// </summary>
    public class DefaultAdaptiveTriggerManager : IAdaptiveTriggerManager
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly IList<IAdaptiveTrigger> _registeredTriggers; 

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultAdaptiveTriggerManager"/> class.
        /// </summary>
        public DefaultAdaptiveTriggerManager()
        {
            _registeredTriggers = new List<IAdaptiveTrigger>();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private IEnumerable<IDynamicTrigger> GetDynamicTriggers()
        {
            return _registeredTriggers.OfType<IDynamicTrigger>();
        }

        private IEnumerable<IDynamicTrigger> GetDynamicTriggers(IEnumerable<VisualStateGroup> groups)
        {
            return from stateGroup in groups
                   from visualState in stateGroup.States
                   from dynamicTrigger in visualState.StateTriggers.OfType<IDynamicTrigger>()
                   select dynamicTrigger;
        }

        private IEnumerable<IDynamicTrigger> GetDynamicTriggers(IEnumerable<StateTriggerBase> triggers)
        {
            return triggers.OfType<IDynamicTrigger>();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IAdaptiveTriggerManager Members

        void IAdaptiveTriggerManager.RegisterTrigger(IAdaptiveTrigger trigger)
        {
            _registeredTriggers.Add(trigger);
        }

        void IAdaptiveTriggerManager.SuspendAllDynamicTriggers()
        {
            foreach (var dynamicTrigger in GetDynamicTriggers())
                dynamicTrigger.SuspendUpdates();
        }

        void IAdaptiveTriggerManager.ResumeAllDynamicTriggers()
        {
            foreach (var dynamicTrigger in GetDynamicTriggers())
                dynamicTrigger.ResumeUpdates();
        }

        void IAdaptiveTriggerManager.ValidateAllDynamicTriggers()
        {
            foreach (var dynamicTrigger in GetDynamicTriggers())
                dynamicTrigger.ForceValidation();
        }

        void IAdaptiveTriggerManager.ResumeAndValidateAllDynamicTriggers()
        {
            foreach (var dynamicTrigger in GetDynamicTriggers())
            {
                dynamicTrigger.ResumeUpdates();
                dynamicTrigger.ForceValidation();
            }
        }

        void IAdaptiveTriggerManager.SuspendDynamicTriggers(IEnumerable<VisualStateGroup> groups)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(groups))
                dynamicTrigger.SuspendUpdates();
        }

        void IAdaptiveTriggerManager.ResumeDynamicTriggers(IEnumerable<VisualStateGroup> groups)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(groups))
                dynamicTrigger.ResumeUpdates();
        }

        void IAdaptiveTriggerManager.ValidateDynamicTriggers(IEnumerable<VisualStateGroup> groups)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(groups))
                dynamicTrigger.ForceValidation();
        }

        void IAdaptiveTriggerManager.ResumeAndValidateDynamicTriggers(IEnumerable<VisualStateGroup> groups)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(groups))
            {
                dynamicTrigger.ResumeUpdates();
                dynamicTrigger.ForceValidation();
            }
        }

        void IAdaptiveTriggerManager.SuspendDynamicTriggers(IEnumerable<StateTriggerBase> triggers)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(triggers))
                dynamicTrigger.SuspendUpdates();
        }

        void IAdaptiveTriggerManager.ResumeDynamicTriggers(IEnumerable<StateTriggerBase> triggers)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(triggers))
                dynamicTrigger.ResumeUpdates();
        }

        void IAdaptiveTriggerManager.ValidateDynamicTriggers(IEnumerable<StateTriggerBase> triggers)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(triggers))
                dynamicTrigger.ForceValidation();
        }

        void IAdaptiveTriggerManager.ResumeAndValidateDynamicTriggers(IEnumerable<StateTriggerBase> triggers)
        {
            foreach (var dynamicTrigger in GetDynamicTriggers(triggers))
            {
                dynamicTrigger.ResumeUpdates();
                dynamicTrigger.ForceValidation();
            }
        }

        #endregion
    }
}