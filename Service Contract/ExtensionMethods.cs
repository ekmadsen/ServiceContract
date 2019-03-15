using System.Collections.Generic;
using System.Text.RegularExpressions;
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


        [UsedImplicitly]
        public static string RemoveControlCharacters(this string Text) => Regex.Replace(Text, @"[\p{C}-[\r\n\t]]+", string.Empty);
    }
}
