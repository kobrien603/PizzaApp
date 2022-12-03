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
            aesAlg.IV = Encoding.UTF8.GetBytes(IVKey);

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
                    result = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(result);
        }

        public static string DecryptString(string encrypted)
        {
            string response = "";
            var encryptedBytes = Encoding.UTF8.GetBytes(encrypted);
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(IVKey);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
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
