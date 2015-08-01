namespace AdaptiveTriggerLibrary.ConditionModifiers.GenericModifiers
{
    /// <summary>
    /// Base interface for modifiers implementing <see cref="IConditionModifier{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericModifier<in T> : IConditionModifier<T>
    {
         
    }
}