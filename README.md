#  SecureStringNAOT
Compile time string encryption with NativeAOT

This example utilises the fact that the [static constructors including static readonly fields are pre-interpreted at compile time](https://github.com/dotnet/corert/pull/8176) when the ILC(NativeAOT compiler) translates the code into native code.

# Limitation
As the strings are a reference-type that are allocated on the heap, they are ultimately baked statically into the compiled executable. Therefore, only stack allocated `Span<char>` can be encrypted.
Any solution is welcomed via PR, however I have yet to find a viable solution as the issue tightly relates to how strings work in .NET.

# Before Decryption
![](https://github.com/ZeroLP/SecureStringNAOT/blob/main/HexeditorStaticAnalysis.jpg)

![](https://github.com/ZeroLP/SecureStringNAOT/blob/main/BeforeDecryption.jpg)

# After Decryption
![](https://github.com/ZeroLP/SecureStringNAOT/blob/main/AfterDecryption.jpg)