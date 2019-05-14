namespace Fibs
{
    using System;
    using System.Numerics;
    using BenchmarkDotNet.Attributes;

    public class Fib
    {
        readonly double phi = (1 + Math.Sqrt(5)) / 2;
        public double ComputeNth(double n) => Math.Floor(Math.Pow(phi, n) / Math.Sqrt(5) + 0.5);
        public double ComputeNth_exp(double n) => Math.Floor(Math.Exp(n * Math.Log(phi)) / Math.Sqrt(5) + 0.5);

        [Benchmark]
        [Arguments(10)]
        public Vector<double> Compute4n(double n) => new Vector<double>(new double[] {
            ComputeNth(n),
            ComputeNth(n + 1),
            ComputeNth(n + 2),
            ComputeNth(n + 3)
        });

        [Benchmark]
        [Arguments(10)]
        public Vector<double> Compute4n_exp(double n) => new Vector<double>(new double[] {
            ComputeNth_exp(n),
            ComputeNth_exp(n + 1),
            ComputeNth_exp(n + 2),
            ComputeNth_exp(n + 3)
        });
    }

    [SimpleJob]
    public class Program
    {
        public static void Main(string[] args)
        {
            //BenchmarkRunner.Run<Fib>();
            int index = 0, numFibs = 25, vecSize = Vector<double>.Count;
            double[] numbers = new double[numFibs];
            var fib = new Fib();

            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                var v = fib.Compute4n_exp(index);
                v.CopyTo(numbers, index);
            }
            if (index != numFibs)
            {
                for (; index < numFibs; ++index)
                {
                    numbers[index] = fib.ComputeNth_exp(index);
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}
