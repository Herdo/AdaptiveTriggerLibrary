namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System.Linq;

    /// <summary>
    /// A modifier where any value of the values must meet the condition.
    /// </summary>
    public class OrModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier Members

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.Any(m => m == condition) ?? false;
        }

        #endregion
    }
}