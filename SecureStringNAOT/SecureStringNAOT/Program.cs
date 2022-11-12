using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace SecureStringNAOT
{
    internal class Program
    {
        private static readonly string HelloWorldEncrypted = stackalloc char[] { 'H', 'e', 'l', 'l', 'o', ',', ' ', 'W', 'o', 'r', 'l', 'd', '!' }.Encrypt();

        static void Main(string[] args)
        {
            Console.WriteLine($"Encrypted string: {HelloWorldEncrypted}, press any key to decrypt.");

            Console.ReadKey();

            Console.WriteLine($"Decrypted string: {HelloWorldEncrypted.Decrypt()}");

            Console.ReadLine();
        }
    }

    public static class SecureStringProvider
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Encrypt(this Span<char> plainStr)
        {
            ReadOnlySpan<char> xorKey = stackalloc char[] { '5', 'f', 'f', '2', '6', 'c', '5', '2', 'f', 'f', '8', 'b', '2', 'f', '9', '4', '8', '1', 'f', '9', '3', '6', '8', '3', '7', '8', 'd', 'e', 'd', 'd', '4', 'd' };

            char[] xorStringChars = new char[plainStr.Length];
            for (int i = 0; i < plainStr.Length; i++)
                xorStringChars[i] = (char)(plainStr[i] ^ xorKey[i % xorKey.Length]);

            return new string(xorStringChars);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Decrypt(this string encryptedString)
        {
            ReadOnlySpan<char> xorKey = stackalloc char[] { '5', 'f', 'f', '2', '6', 'c', '5', '2', 'f', 'f', '8', 'b', '2', 'f', '9', '4', '8', '1', 'f', '9', '3', '6', '8', '3', '7', '8', 'd', 'e', 'd', 'd', '4', 'd' };

            char[] xorStringChars = new char[encryptedString.Length];
            for (int i = 0; i < encryptedString.Length; i++)
                xorStringChars[i] = (char)(encryptedString[i] ^ xorKey[i % xorKey.Length]);

            return new string(xorStringChars);
        }
    }
}