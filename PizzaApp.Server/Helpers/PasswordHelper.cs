using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Server.Helpers
{
    public static class PasswordHelper
    {
        private static readonly byte[] Salt = new byte[16];
        private static readonly string Secret = "Fub3#e!LV#58^wr";
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        /// <summary>
        /// validate password with argon2 c# library
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string passwordHash)
        {
            var config = new Argon2Config
            {
                Password = Encoding.UTF8.GetBytes(password),
                Secret = Encoding.UTF8.GetBytes(Secret)
            };

            SecureArray<byte> hashB = null;
            try
            {
                if (config.DecodeString(passwordHash, out hashB) && hashB != null)
                {
                    var argon2ToVerify = new Argon2(config);
                    
                    using var hashToVerify = argon2ToVerify.Hash();
                    if (Argon2.FixedTimeEquals(hashB, hashToVerify))
                    {
                        return true;
                    }

                    hashToVerify.Dispose();
                }
            }
            finally
            {
                hashB?.Dispose();
            }

            return false;
        }

        /// <summary>
        /// has requested password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CreateHashPassword(string password)
        {
            string hashString = "";
            try
            {
                Rng.GetBytes(Salt);

                var config = new Argon2Config
                {
                    Type = Argon2Type.DataIndependentAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = 10,
                    MemoryCost = 32768,
                    Lanes = 5,
                    Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                    Password = Encoding.UTF8.GetBytes(password),
                    Salt = Salt, // >= 8 bytes if not null
                    Secret = Encoding.UTF8.GetBytes(Secret),
                    HashLength = 20 // >= 4
                };

                var argon2A = new Argon2(config);

                using SecureArray<byte> hashA = argon2A.Hash();
                hashString = config.EncodeString(hashA.Buffer);

                hashA.Dispose();
            }
            catch (Exception e)
            {
                // todo - log message
            }

            return hashString;
        }
    }
}
