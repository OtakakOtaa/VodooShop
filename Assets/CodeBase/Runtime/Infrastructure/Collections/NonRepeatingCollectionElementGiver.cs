using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Runtime.Infrastructure.Collections
{
    public sealed class NonRepeatingCollectionElementGiver<TSource>
    {
        private List<TSource> _nonUsedCollection;
        private readonly List<TSource> _usedElements = new ();

        public NonRepeatingCollectionElementGiver(IEnumerable<TSource> collection)
            => _nonUsedCollection = collection.ToList();

        public bool GetNext(out TSource element, bool restartContainer = false)
        {
            element = default;
            
            var collectionEnded = _nonUsedCollection.Count == 0;
            if (collectionEnded && restartContainer) 
                Restart();
            if (collectionEnded && restartContainer is false) 
                return false;
            
            element = _nonUsedCollection[RandomIndex];
            _usedElements.Add(element);
            _nonUsedCollection.Remove(element);
            
            return true;
        }
        
        private int RandomIndex => new Random().Next(0, _nonUsedCollection.Count);

        private void Restart()
        {
            _nonUsedCollection = _usedElements;
            _usedElements.Clear();
        }
    }
}