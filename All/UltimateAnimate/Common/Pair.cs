namespace UltimateAnimate.Common
{
    public struct Pair<TValue1, TValue2>
    {
        public Pair(TValue1 value1, TValue2 value2) : this()
        {
            Value2 = value2;
            Value1 = value1;
        }

        public TValue1 Value1 { get; private set; }
        public TValue2 Value2 { get; private set; }
    }
}
