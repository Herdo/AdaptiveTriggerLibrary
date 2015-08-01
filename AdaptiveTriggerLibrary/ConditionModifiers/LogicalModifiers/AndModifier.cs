namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System.Linq;

    /// <summary>
    /// A modifier where all values must meet the condition.
    /// </summary>
    public class AndModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.All(m => m == condition) ?? false;
        }

        #endregion
    }
}