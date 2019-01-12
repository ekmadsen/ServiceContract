using System.Collections.Generic;
using JetBrains.Annotations;


namespace ErikTheCoder.ServiceContract
{
    [UsedImplicitly]
    public static class ExtensionMethods
    {
        [UsedImplicitly]
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> Tuple, out T1 Key, out T2 Value)
        {
            Key = Tuple.Key;
            Value = Tuple.Value;
        }
    }
}
