namespace AdaptiveTriggerLibrary.Extensibility
{
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Triggers;

    /// <summary>
    /// Manages <see cref="IAdaptiveTrigger"/> instances.
    /// </summary>
    public interface IAdaptiveTriggerManager
    {
        /// <summary>
        /// Registers the <paramref name="trigger"/>.
        /// </summary>
        /// <param name="trigger">The <see cref="IAdaptiveTrigger"/> to register.</param>
        void RegisterTrigger(IAdaptiveTrigger trigger);

        /// <summary>
        /// Suspends all registered dynamic triggers.
        /// </summary>
        void SuspendAllDynamicTriggers();

        /// <summary>
        /// Resumes all registered dynamic triggers.
        /// </summary>
        void ResumeAllDynamicTriggers();

        /// <summary>
        /// Forces a validation for all registered dynamic triggers.
        /// </summary>
        void ValidateAllDynamicTriggers();

        /// <summary>
        /// Resumes all registered dynamic triggers and forces a validation for each trigger.
        /// </summary>
        void ResumeAndValidateAllDynamicTriggers();

        /// <summary>
        /// Suspends all dynamic triggers in the <paramref name="groups"/>.
        /// </summary>
        /// <param name="groups">The groups containing dynamic triggers.</param>
        void SuspendDynamicTriggers(IEnumerable<VisualStateGroup> groups);

        /// <summary>
        /// Resumes all dynamic triggers in the <paramref name="groups"/>.
        /// </summary>
        /// <param name="groups">The groups containing dynamic triggers.</param>
        void ResumeDynamicTriggers(IEnumerable<VisualStateGroup> groups);

        /// <summary>
        /// Forces a validation for all dynamic triggers in the <paramref name="groups"/>.
        /// </summary>
        /// <param name="groups">The groups containing dynamic triggers.</param>
        void ValidateDynamicTriggers(IEnumerable<VisualStateGroup> groups);

        /// <summary>
        /// Resumes all dynamic triggers in the <paramref name="groups"/> and forces a validation for each trigger.
        /// </summary>
        /// <param name="groups">The groups containing dynamic triggers.</param>
        void ResumeAndValidateDynamicTriggers(IEnumerable<VisualStateGroup> groups);

        /// <summary>
        /// Suspends all dynamic triggers in the <paramref name="triggers"/>.
        /// </summary>
        /// <param name="triggers">The triggers containing dynamic triggers.</param>
        void SuspendDynamicTriggers(IEnumerable<StateTriggerBase> triggers);

        /// <summary>
        /// Resumes all dynamic triggers in the <paramref name="triggers"/>.
        /// </summary>
        /// <param name="triggers">The triggers containing dynamic triggers.</param>
        void ResumeDynamicTriggers(IEnumerable<StateTriggerBase> triggers);

        /// <summary>
        /// Forces a validation for all dynamic triggers in the <paramref name="triggers"/>.
        /// </summary>
        /// <param name="triggers">The triggers containing dynamic triggers.</param>
        void ValidateDynamicTriggers(IEnumerable<StateTriggerBase> triggers);

        /// <summary>
        /// Resumes all dynamic triggers in the <paramref name="triggers"/> and forces a validation for each trigger.
        /// </summary>
        /// <param name="triggers">The triggers containing dynamic triggers.</param>
        void ResumeAndValidateDynamicTriggers(IEnumerable<StateTriggerBase> triggers);
    }
}