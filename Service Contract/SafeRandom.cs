using System;
using System.Security.Cryptography;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    [UsedImplicitly]
    public class SafeRandom : ISafeRandom
    {
        private RNGCryptoServiceProvider _random;
        private object _lock;
        private bool _disposed;


        public SafeRandom()
        {
            _random = new RNGCryptoServiceProvider();
            _lock = new object();
        }


        ~SafeRandom()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool Disposing)
        {
            if (_disposed) return;
            // Release unmanaged resources.
            lock (_lock)
            {
                _random.Dispose();
                _random = null;
            }
            if (Disposing)
            {
                // Release managed resources.
                _lock = null;
            }
            _disposed = true;
        }


        public int Next(int InclusiveMin, int ExclusiveMax)
        {
            int range = ExclusiveMax - InclusiveMin;
            byte[] randomBytes = new byte[sizeof(int)];
            NextBytes(randomBytes);
            uint random = BitConverter.ToUInt32(randomBytes, 0);
            return InclusiveMin + (int)(random % range);
        }


        public void NextBytes(byte[] Bytes)
        {
            lock (_lock)
            {
                _random.GetBytes(Bytes);
            }
        }
    }
}