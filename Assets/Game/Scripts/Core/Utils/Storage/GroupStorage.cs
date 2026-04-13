using System;
using System.Collections.Generic;

namespace TowerDefence.Core.Utils.Storage
{
    public sealed class GroupStorage<TObject> where TObject : class
    {
        private readonly Dictionary<Type, TObject> _dictionary;
        
        public IReadOnlyDictionary<Type, TObject> Dictionary => _dictionary;

        public GroupStorage() => _dictionary = new();

        public GroupStorage(int capacity) => _dictionary = new(capacity);

        public bool TryGet<TGet>(Type slot, out TGet value) where TGet : class
        {
            if (!_dictionary.TryGetValue(slot, out var result) || result is not TGet get)
            {
                value = null;
                return false;
            }

            value = get;
            return true;
        }

        public void Add(Type type, TObject value)
        {
            _dictionary.TryAdd(type, value);
        }

        public void Remove(Type type)
        {
            _dictionary.Remove(type);
        }
    }
}