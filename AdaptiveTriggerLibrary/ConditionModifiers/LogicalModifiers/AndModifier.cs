namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System.Linq;

    public class AndModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier

        public bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.All(m => m == condition) ?? false;
        }

        #endregion
    }
}