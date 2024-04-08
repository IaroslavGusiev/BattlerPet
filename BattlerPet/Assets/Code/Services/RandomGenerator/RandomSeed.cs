using System;
using System.Security.Cryptography;

namespace Code.Services
{
    public static class RandomSeed
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
        
        public static int Time() => 
            DateTime.UtcNow.GetHashCode();

        public static int Guid() => 
            Environment.TickCount ^ System.Guid.NewGuid().GetHashCode();
        
        public static int Crypto()
        {
            var bytes = new byte[4];
            RandomNumberGenerator.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}