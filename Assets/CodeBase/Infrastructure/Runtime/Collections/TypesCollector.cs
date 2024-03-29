using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Infrastructure.Runtime.Collections
{
    public sealed class TypesCollector
    {
        private readonly Type[] _types;

        public TypesCollector(IEnumerable<Type> types)
        {
            _types = types.ToArray();
        }

        public bool HasType(object target) 
            => _types.FirstOrDefault( t => t == target.GetType()) != default;

        public bool HasType<TType>()
            => _types.OfType<Type>().Any();
    }
}