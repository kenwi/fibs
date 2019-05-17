namespace fibs
{
    using System;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main(string[] args)
        {
#if RELEASE
            BenchmarkRunner.Run<Benchmark>();
#endif

#if DEBUG
        var b = new Benchmark();
        var n = b.RunDoubles();
        var n2 = b.RunFloats();
        var n3 = b.RunGenericDoubles();
        var n4 = b.RunGenericFloats();
#endif
        }
    }
}
