namespace AdaptiveTriggerLibrary.Triggers
{
    /// <summary>
    /// Triggers where the state of <see cref="IAdaptiveTrigger.IsActive"/>
    /// won't change during the application lifetime.
    /// </summary>
    public interface IStaticTrigger : IAdaptiveTrigger
    {
         
    }
}