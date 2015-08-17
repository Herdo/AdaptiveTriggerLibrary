namespace AdaptiveTriggerLibrary.Triggers
{
    /// <summary>
    /// Triggers where the state of <see cref="IAdaptiveTrigger.IsActive"/>
    /// may change during the application lifetime.
    /// </summary>
    public interface IDynamicTrigger : IAdaptiveTrigger
    {
        /// <summary>
        /// Forces a validation on the current trigger state.
        /// </summary>
        void ForceValidation();

        /// <summary>
        /// Suspends all updates of the trigger by detaching the event handlers.
        /// </summary>
        void SuspendUpdates();

        /// <summary>
        /// Resumes all updates of the trigger by attaching the event handlers.
        /// </summary>
        /// <remarks>After resuming to receive updates, you should call
        /// <see cref="ForceValidation"/> to update the current state.</remarks>
        void ResumeUpdates();
    }
}