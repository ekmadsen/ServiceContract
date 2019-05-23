using System;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    public interface ISafeRandom : IDisposable
    {
        [UsedImplicitly] int Next(int InclusiveMin, int ExclusiveMax);
        [UsedImplicitly] void NextBytes(byte[] Bytes);
    }
}
