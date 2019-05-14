namespace BinetsFormula
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Numerics;

    class Program
    {
        static void Main(string[] args)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double computeNth(double n) => Math.Floor(Math.Pow(phi, n) / Math.Sqrt(5) + 0.5);
            Vector<double> compute4n(double n) => new Vector<double>(new double[] {
                computeNth(n),
                computeNth(n + 1),
                computeNth(n + 2),
                computeNth(n + 3)
            });
                        
            int i = 0, numFibs = 10, vecSize = Vector<double>.Count;
            double [] result = new double[numFibs];
            
            for (; i < numFibs - vecSize + 1; i += vecSize)
            {
                var v = compute4n(i);
                v.CopyTo(result, i);
            }
            if (i != numFibs)
            {
                for (; i < numFibs; i++)
                {
                    var v = computeNth(i);
                    result[i] = v;
                }
            }
            Console.WriteLine(string.Join(", ", result));
        }
    }
}
