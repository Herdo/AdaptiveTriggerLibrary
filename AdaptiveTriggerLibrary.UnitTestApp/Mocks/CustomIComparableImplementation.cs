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
            if (other == null)
                throw new ArgumentException("Object must be of type CustomIComparableImplementation.");

            return Value.CompareTo(other.Value);
        }
    }
}