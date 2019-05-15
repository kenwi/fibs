// * Summary *

BenchmarkDotNet=v0.11.5
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT


|        Method |  n |      Mean |     Error |    StdDev |
|-------------- |--- |----------:|----------:|----------:|
|      ComputeN | 10 |  28.26 ns | 0.7266 ns | 0.6797 ns |
|  ComputeN_exp | 10 |  17.07 ns | 0.2139 ns | 0.1786 ns |
|     Compute4n | 10 |  76.44 ns | 1.1643 ns | 1.0890 ns |
|     ComputeNf | 10 |  10.34 ns | 0.1812 ns | 0.1695 ns |
| ComputeNf_exp | 10 |  14.28 ns | 0.2755 ns | 0.2577 ns |
|   Compute8n_f | 10 | 117.49 ns | 2.3617 ns | 2.5270 ns |
|  RunWithFloats | 48.37 us | 0.8567 us | 0.7595 us |
| RunWithDoubles | 20.75 us | 0.4110 us | 0.4221 us |
