﻿namespace fibs
{
    using System;
    using System.Diagnostics;
    using System.Numerics;
    using BenchmarkDotNet.Attributes;

    public class BenchmarkComputation<T>
    {
        public Action<T[], int> ComputeMultiple;
        public Action<T[], int> ComputeSingle;
    }

    public class Benchmark
    {
        readonly double phi_d = (1 + Math.Sqrt(5)) * 0.5;
        readonly float phi_f = (1 + MathF.Sqrt(5)) * 0.5f;
        int index, vecSize, numFibs = 1000;

        double[] doubles;
        float[] floats;
        BenchmarkComputation<double> computation_d;
        BenchmarkComputation<float> computation_f;

        public Benchmark()
        {
            doubles = new double[numFibs];
            computation_d = new BenchmarkComputation<double>();
            computation_d.ComputeMultiple = (numbers, index) => Compute4n_d(index).CopyTo(numbers, index);
            computation_d.ComputeSingle = (numbers, index) => numbers[index] = ComputeN_dexp(index);

            floats = new float[numFibs];
            computation_f = new BenchmarkComputation<float>();
            computation_f.ComputeMultiple = (numbers, index) => Compute8n_f(index).CopyTo(numbers, index);
            computation_f.ComputeSingle = (numbers, index) => numbers[index] = ComputeN_fexp(index);
        }
        
        [Benchmark]
        [Arguments(10)]
        public double ComputeN_d(double n) => Math.Floor(Math.Pow(phi_d, n) / Math.Sqrt(5) + 0.5);

        [Benchmark]
        [Arguments(10)]
        public float ComputeN_f(float n) => MathF.Floor(MathF.Pow(phi_f, n) / MathF.Sqrt(5) + 0.5f);

        [Benchmark]
        [Arguments(10)]
        public float ComputeN_fexp(float n) => MathF.Floor(MathF.Exp(n * MathF.Log(phi_f)) / MathF.Sqrt(5) + 0.5f);

        [Benchmark]
        [Arguments(10)]
        public double ComputeN_dexp(double n) => Math.Floor(Math.Exp(n * Math.Log(phi_d)) / Math.Sqrt(5) + 0.5);

        [Benchmark]
        [Arguments(10)]
        public Vector<double> Compute4n_d(double n) => new Vector<double>(new double[] {
            ComputeN_dexp(n),
            ComputeN_dexp(n + 1),
            ComputeN_dexp(n + 2),
            ComputeN_dexp(n + 3)
        });

        [Benchmark]
        [Arguments(10)]
        public Vector<float> Compute8n_f(float n) => new Vector<float>(new float[] {
            ComputeN_fexp(n),
            ComputeN_fexp(n + 1),
            ComputeN_fexp(n + 2),
            ComputeN_fexp(n + 3),
            ComputeN_fexp(n + 4),
            ComputeN_fexp(n + 5),
            ComputeN_fexp(n + 6),
            ComputeN_fexp(n + 7)
        });

        [Benchmark]
        public double[] RunDoubles()
        {
            vecSize = Vector<double>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                Compute4n_d(index).CopyTo(doubles, index);
            }
            while (index != numFibs)
            {
                doubles[index++] = ComputeN_dexp(index);
            }
            index = 0;
            return doubles;
        }

        [Benchmark]
        public float[] RunFloats()
        {
            vecSize = Vector<float>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                Compute8n_f(index).CopyTo(floats, index);
            }
            while (index != numFibs)
            {
                floats[index++] = ComputeN_fexp(index);
            }
            index = 0;
            return floats;
        }

        [Benchmark]
        public double[] RunGenericDoubles()
        {
            vecSize = Vector<double>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                computation_d.ComputeMultiple(doubles, index);
            }
            while (index != numFibs)
            {
                computation_d.ComputeSingle(doubles, index++);
            }
            index = 0;
            return doubles;
        }

        [Benchmark]
        public float[] RunGenericFloats()
        {
            vecSize = Vector<float>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                computation_f.ComputeMultiple(floats, index);
            }
            while (index != numFibs)
            {
                computation_f.ComputeSingle(floats, index++);
            }
            index = 0;
            return floats;
        }
    }
}
