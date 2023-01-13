using System.Security.Cryptography;
using System.Text;

namespace PizzaApp.Server.Helpers
{
    public static class EncryptionHelper
    {
        const string IVKey = "3ApP36uq9FeDyf0B";
        const string Key = "KynvwFqLPSC7SBaFc@Y%BbGhgt5p6R9j";
        
        public static string EncryptString(string text)
        {
            byte[] result;

            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(Key);
            //aesAlg.IV = Encoding.UTF8.GetBytes(IVKey);

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(text);
                    }

                    var decryptedContent = msEncrypt.ToArray();

                    result = new byte[aesAlg.IV.Length + decryptedContent.Length];

                    Buffer.BlockCopy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
                    Buffer.BlockCopy(decryptedContent, 0, result, aesAlg.IV.Length, decryptedContent.Length);

                    //result = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(result);
        }

        public static string DecryptString(string encrypted)
        {
            string response = "";

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                //aesAlg.IV = Encoding.UTF8.GetBytes(IVKey);

                var iv = new byte[16];
                var cipher = new byte[16];
                var fullCipher = Convert.FromBase64String(encrypted);

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

                // Create a decryptor to perform the stream transform.
                using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipher))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            response = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return response;
        }
    }
}
