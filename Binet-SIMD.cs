﻿namespace fibs
{
    using System;
    using System.Numerics;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    public class Fib
    {
        readonly double phi = (1 + Math.Sqrt(5)) * 0.5;
        readonly float phif = (1 + MathF.Sqrt(5)) * 0.5f;

        [Benchmark]
        [Arguments(10)]
        public double ComputeN(double n) => Math.Floor(Math.Pow(phi, n) / Math.Sqrt(5) + 0.5);

        [Benchmark]
        [Arguments(10)]
        public double ComputeN_exp(double n) => Math.Floor(Math.Exp(n * Math.Log(phi)) / Math.Sqrt(5) + 0.5);

        [Benchmark]
        [Arguments(10)]
        public float ComputeNf(float n) => MathF.Floor(MathF.Pow(phif, n) / MathF.Sqrt(5) + 0.5f);

        [Benchmark]
        [Arguments(10)]
        public float ComputeNf_exp(float n) => MathF.Floor(MathF.Exp(n * MathF.Log(phif)) / MathF.Sqrt(5) + 0.5f);

        [Benchmark]
        [Arguments(10)]
        public Vector<double> Compute4n(double n) => new Vector<double>(new double[] {
            ComputeN_exp(n),
            ComputeN_exp(n + 1),
            ComputeN_exp(n + 2),
            ComputeN_exp(n + 3)
        });

        [Benchmark]
        [Arguments(10)]
        public Vector<float> Compute8n_f(float n) => new Vector<float>(new float[] {
            ComputeNf_exp(n),
            ComputeNf_exp(n + 1),
            ComputeNf_exp(n + 2),
            ComputeNf_exp(n + 3),
            ComputeNf_exp(n + 4),
            ComputeNf_exp(n + 5),
            ComputeNf_exp(n + 6),
            ComputeNf_exp(n + 7)
        });
    }

    [SimpleJob]
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Fib>();
            {
                int index = 0, numFibs = 10, vecSize = Vector<float>.Count;
                float[] numbers = new float[numFibs];
                var fib = new Fib();

                for (; index < numFibs - vecSize + 1; index += vecSize)
                {
                    fib.Compute8n_f(index).CopyTo(numbers, index);
                }
                if (index != numFibs)
                {
                    for (; index < numFibs; ++index)
                    {
                        numbers[index] = fib.ComputeNf_exp(index);
                    }
                }
                Console.WriteLine(string.Join(", ", numbers));
            }
            Console.WriteLine();
            {
                int index = 0, numFibs = 10, vecSize = Vector<double>.Count;
                double[] numbers = new double[numFibs];
                var fib = new Fib();
                for (; index < numFibs - vecSize + 1; index += vecSize)
                {
                    fib.Compute4n(index).CopyTo(numbers, index);
                }
                if (index != numFibs)
                {
                    for (; index < numFibs; ++index)
                    {
                        numbers[index] = fib.ComputeN_exp(index);
                    }
                }
                Console.WriteLine(string.Join(", ", numbers));
            }
        }
    }
}