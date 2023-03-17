using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate
{
    public abstract class TableTemplate
    {
        public readonly Dictionary<string, int> Keys = new();

        public string IdKey { get; protected set; }
        public abstract bool HasBeenDetected { get; }
        public abstract bool ThisReadKey(string comparedField);

        public void BindKeyToColumn(string keyId, int readKeyPosition)
            => Keys[keyId] = readKeyPosition;
        
        public bool TryGetKey(int position, out string key)
        {
            key = string.Empty;
                
            if (Keys.Values.Contains(position) is false) return false;
            key = Keys.First(pair => pair.Value == position).Key;
            return true;
        }
    }
}