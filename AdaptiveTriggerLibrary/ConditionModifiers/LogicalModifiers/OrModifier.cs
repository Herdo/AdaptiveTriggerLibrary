namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System.Linq;

    public class OrModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier Members

        public bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.Any(m => m == condition) ?? false;
        }

        #endregion
    }
}