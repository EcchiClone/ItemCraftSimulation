using System;
using System.Collections.Generic;

public class Utils
{
    public static bool IsSameDict<T1, T2>(Dictionary<T1, T2> a, Dictionary<T1, T2> b)
        where T1 : notnull
        where T2 : IEquatable<T2>
    {
        if (a.Count != b.Count)
            return false;

        foreach (var kv in a)
        {
            if (!b.TryGetValue(kv.Key, out T2 value) || !kv.Value.Equals(value))
                return false;
        }

        return true;
    }
}