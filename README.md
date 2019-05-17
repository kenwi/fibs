// * Summary *

BenchmarkDotNet=v0.11.5
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview3-27503-5 (CoreCLR 4.6.27422.72, CoreFX 4.7.19.12807), 64bit RyuJIT

|            Method |  n |         Mean |       Error |        StdDev |
|------------------ |--- |-------------:|------------:|--------------:|
|        RunDoubles |  ? | 20,228.82 ns | 475.3984 ns |   711.5539 ns |
|         RunFloats |  ? | 48,781.02 ns | 952.1425 ns | 1,096.4888 ns |
| RunGenericDoubles |  ? | 22,229.58 ns | 192.6551 ns |   160.8757 ns |
|  RunGenericFloats |  ? | 49,733.11 ns | 966.6905 ns |   992.7200 ns |
|        ComputeN_d | 10 |     27.57 ns |   0.6113 ns |     0.5419 ns |
|        ComputeN_f | 10 |     11.28 ns |   0.2628 ns |     0.2459 ns |
|     ComputeN_fexp | 10 |     14.41 ns |   0.3311 ns |     0.3400 ns |
|     ComputeN_dexp | 10 |     17.67 ns |   0.3013 ns |     0.2818 ns |
|       Compute4n_d | 10 |     76.58 ns |   1.4503 ns |     1.3566 ns |
|       Compute8n_f | 10 |    117.08 ns |   2.3343 ns |     2.4977 ns |