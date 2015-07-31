namespace AdaptiveTriggerLibrary.UnitTestApp.Mocks
{
    using System;

    internal class CustomIComparableImplementation : IComparable
    {
        public int Value { get; }

        public CustomIComparableImplementation(int value)
        {
            Value = value;
        }

        public int CompareTo(object obj)
        {
            var other = obj as CustomIComparableImplementation;
            return other == null
                ? 1
                : Value.CompareTo(other.Value);
        }
    }
}