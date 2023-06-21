using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Collections
{
    [Serializable]
    public sealed class SyncDictionary<TKey, TValue>
    {
        [SerializeField] private List<Pair> _items;
        
        public SyncDictionary(IEnumerable<TKey> keys, IEnumerable<TValue> values)
        {
            _items = keys
                .Select((k, i) => new KeyValuePair<TKey,TValue>(k, values.ElementAt(i)))
                .Select(p => new Pair { Key = p.Key, Value = p.Value })
                .ToList();
        }

        public IEnumerable<TKey> Keys => _items.Select(p => p.Key);
        public IEnumerable<TValue> Values => _items.Select(p => p.Value);

        [Serializable] private class Pair
        {
            [SerializeField] public TKey Key;
            [SerializeField] public TValue Value;
        }
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
                dictionary[sources.Keys.ElementAt(i)] = sources.Values.ElementAt(i);
            return dictionary;
        }
    }
}