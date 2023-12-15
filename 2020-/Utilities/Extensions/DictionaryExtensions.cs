using System.Collections.Generic;

namespace AdventOfCode.Utilities.Extensions;

public static class DictionaryExtensions
{
    public static void AddSafely<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key].Add(value);
        else
            dictionary.Add(key, new List<TValue>{value});
    }
    
    public static void AddSafely<TKey>(this Dictionary<TKey, long> dictionary, TKey key, long value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += value;
        else
            dictionary.Add(key, value);
    }
    
    public static void AddSafely<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += value;
        else
            dictionary.Add(key, value);
    }
}