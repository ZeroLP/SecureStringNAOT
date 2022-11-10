using System.Runtime.CompilerServices;

namespace SecureStringNAOT
{
    internal class Program
    {
        private static readonly string EncryptedString = "Hello World!".Encrypt();

        /*static Program()
        {
            EncryptedString = "Hello World!".Encrypt();
        }*/

        static void Main(string[] args)
        {
            Console.WriteLine($"Encrypted: {EncryptedString}, press any key to decrypt.");
            Console.ReadLine();
            Console.WriteLine($"Decrypted: {EncryptedString.Decrypt()}");

            Console.ReadLine();
        }
    }

    public static class SecureStringProvider
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Encrypt(this string plainStr)
        {
            string xorKey = "5ff26c52ff8b2f9481f9368378dedd4d";

            char[] xorStringChars = new char[plainStr.Length];
            for (int i = 0; i < plainStr.Length; i++)
                xorStringChars[i] = (char)(plainStr[i] ^ xorKey[i % xorKey.Length]);

            //Patch out the original string in memory to a nullptr
            unsafe
            {
                fixed (char* cPtr = plainStr)
                {
                    *(IntPtr*)cPtr = 0;
                }
            }

            return new string(xorStringChars);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Decrypt(this string encryptedString)
        {
            string xorKey = "5ff26c52ff8b2f9481f9368378dedd4d";

            char[] xorStringChars = new char[encryptedString.Length];
            for (int i = 0; i < encryptedString.Length; i++)
                xorStringChars[i] = (char)(encryptedString[i] ^ xorKey[i % xorKey.Length]);

            return new string(xorStringChars);
        }
    }
}