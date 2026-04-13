using System;
using System.Collections.Generic;

namespace TowerDefence.Core.Utils.Helpers
{
    public static class DictionaryExtensions
    {
        public static bool Get<TKey, TValue>(this Dictionary<Type, TValue> dict, out TKey result) where TKey : class
        {
            result = null;

            if (!dict.TryGetValue(typeof(TKey), out var value)) 
                return false;
            
            result = value as TKey;
            return true;

        }
    }
}