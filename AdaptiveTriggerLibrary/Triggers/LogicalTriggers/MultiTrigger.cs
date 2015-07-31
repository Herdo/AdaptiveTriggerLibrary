namespace AdaptiveTriggerLibrary.Triggers.LogicalTriggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ConditionModifiers.LogicalModifiers;

    /// <summary>
    /// This trigger activates, if any or all (depending on <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.ConditionModifier"/>)
    /// of the specified <see cref="Triggers"/> matches the <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class MultiTrigger : AdaptiveTriggerBase<bool, ILogicalModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private IList<IAdaptiveTrigger> _triggers;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the triggers.
        /// </summary>
        public IList<IAdaptiveTrigger> Triggers
        {
            get { return _triggers; }
            set
            {
                if (_triggers != null)
                    foreach (var trigger in _triggers)
                        trigger.IsActiveChanged -= Trigger_IsActiveChanged;
                _triggers = value;
                if (_triggers != null)
                    foreach (var trigger in _triggers)
                        trigger.IsActiveChanged += Trigger_IsActiveChanged;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTrigger"/> class.
        /// Default condition: true.
        /// Default modifier: <see cref="AndModifier"/>.
        /// </summary>
        public MultiTrigger()
            : base(true, new AndModifier())
        {
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool GetCurrentValue()
        {
            if (Triggers == null
             || Triggers.Count == 0)
            {
                return false;
            }
            return ConditionModifier.IsConditionMet(Condition, Triggers.Select(m => m.IsActive).ToArray());
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void Trigger_IsActiveChanged(object sender, EventArgs e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}