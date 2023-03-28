using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Runtime.Infrastructure
{
    [Serializable]
    public sealed class SyncDictionary<TKey, TValue>
    {
        [SerializeField] private List<TKey> _keys;
        [SerializeField] private List<TValue> _values;

        public SyncDictionary(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            _keys = keys.ToList();
            _values = values.ToList();
        }

        public List<TKey> Keys => _keys;
        public List<TValue> Values => _values;
    }

    public static class ExtensionForSyncDictionary
    {
        public static SyncDictionary<TKey, TValue> ToSyncDictionary<TSource, TKey, TValue>(
            this IEnumerable<TSource> sources,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector)
            => new(sources.Select(keySelector), sources.Select(valueSelector));

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this SyncDictionary<TKey, TValue> sources)
        {
            Dictionary<TKey, TValue> dictionary = new();
            for (var i = 0; i < sources.Values.Count(); i++)
                dictionary[sources.Keys[i]] = sources.Values[i];
            return dictionary;
        }
    }
}