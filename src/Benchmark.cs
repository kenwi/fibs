
namespace fibs
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
        protected int index, vecSize, numFibs = 1000;
        
        float[] floatNumbers;
        double[] doubleNumbers;
        
        BenchmarkComputation<float> genericFloatComputation;
        BenchmarkComputation<double> genericDoubleComputation;

        public Benchmark()
        {
            doubleNumbers = new double[numFibs];
            genericDoubleComputation = new BenchmarkComputation<double>();
            genericDoubleComputation.ComputeMultiple = (numbers, index) => Compute4n_d(index).CopyTo(numbers, index);
            genericDoubleComputation.ComputeSingle = (numbers, index) => numbers[index] = ComputeN_dexp(index);

            floatNumbers = new float[numFibs];
            genericFloatComputation = new BenchmarkComputation<float>();
            genericFloatComputation.ComputeMultiple = (numbers, index) => Compute8n_f(index).CopyTo(numbers, index);
            genericFloatComputation.ComputeSingle = (numbers, index) => numbers[index] = ComputeN_fexp(index);
        }
        
        [Benchmark]
        public double[] RunDoubles()
        {
            vecSize = Vector<double>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                Compute4n_d(index).CopyTo(doubleNumbers, index);
            }
            while (index != numFibs)
            {
                doubleNumbers[index++] = ComputeN_dexp(index);
            }
            index = 0;
            return doubleNumbers;
        }

        [Benchmark]
        public float[] RunFloats()
        {
            vecSize = Vector<float>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                Compute8n_f(index).CopyTo(floatNumbers, index);
            }
            while (index != numFibs)
            {
                floatNumbers[index++] = ComputeN_fexp(index);
            }
            index = 0;
            return floatNumbers;
        }

        [Benchmark]
        public double[] RunGenericDoubles()
        {
            vecSize = Vector<double>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                genericDoubleComputation.ComputeMultiple(doubleNumbers, index);
            }
            while (index != numFibs)
            {
                genericDoubleComputation.ComputeSingle(doubleNumbers, index++);
            }
            index = 0;
            return doubleNumbers;
        }

        [Benchmark]
        public float[] RunGenericFloats()
        {
            vecSize = Vector<float>.Count;
            for (; index < numFibs - vecSize + 1; index += vecSize)
            {
                genericFloatComputation.ComputeMultiple(floatNumbers, index);
            }
            while (index != numFibs)
            {
                genericFloatComputation.ComputeSingle(floatNumbers, index++);
            }
            index = 0;
            return floatNumbers;
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
    }
}
