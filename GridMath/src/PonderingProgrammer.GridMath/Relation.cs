namespace PonderingProgrammer.GridMath
{
    public readonly struct Relation
    {
        public readonly IntervalAnchor First;
        public readonly IntervalAnchor Second;

        public static Relation StartToStart() => new Relation(IntervalAnchor.Start, IntervalAnchor.Start);
        public static Relation StartToCenter() => new Relation(IntervalAnchor.Start, IntervalAnchor.Center);
        public static Relation StartToEnd() => new Relation(IntervalAnchor.Start, IntervalAnchor.End);
        
        public static Relation CenterToStart() => new Relation(IntervalAnchor.Center, IntervalAnchor.Start);
        public static Relation CenterToCenter() => new Relation(IntervalAnchor.Center, IntervalAnchor.Center);
        public static Relation CenterToEnd() => new Relation(IntervalAnchor.Center, IntervalAnchor.End);
        
        public static Relation EndToStart() => new Relation(IntervalAnchor.End, IntervalAnchor.Start);
        public static Relation EndToCenter() => new Relation(IntervalAnchor.End, IntervalAnchor.Center);
        public static Relation EndToEnd() => new Relation(IntervalAnchor.End, IntervalAnchor.End);
        
        public Relation(IntervalAnchor first, IntervalAnchor second)
        {
            First = first;
            Second = second;
        }
    }
}
