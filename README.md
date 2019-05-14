// * Summary *

BenchmarkDotNet=v0.11.5
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT


|        Method |  n |      Mean |     Error |    StdDev |
|-------------- |--- |----------:|----------:|----------:|
|      ComputeN | 10 |  27.98 ns | 0.5942 ns | 0.5558 ns |
|  ComputeN_exp | 10 |  16.92 ns | 0.1695 ns | 0.1416 ns |
|     ComputeNf | 10 |  10.33 ns | 0.1817 ns | 0.1611 ns |
| ComputeNf_exp | 10 |  13.80 ns | 0.2078 ns | 0.1842 ns |
|     Compute4n | 10 |  75.21 ns | 0.7478 ns | 0.6245 ns |
|   Compute8n_f | 10 | 116.08 ns | 1.3143 ns | 1.2294 ns |
