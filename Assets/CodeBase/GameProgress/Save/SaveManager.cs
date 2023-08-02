using System;
using UnityEngine;

namespace CodeBase.GameProgress.Save
{
    public sealed class SaveManager
    {
        private const string SaveKey = "saves";

        public PersistentSaveFormat Load() 
            => JsonUtility.FromJson<PersistentSaveFormat>(PlayerPrefs.GetString(SaveKey));

        public bool IsFirstSession
            => PlayerPrefs.GetString(SaveKey, defaultValue: string.Empty) == string.Empty; 

        
        [Serializable] public sealed class PersistentSaveFormat
        {
        }
    }
}
