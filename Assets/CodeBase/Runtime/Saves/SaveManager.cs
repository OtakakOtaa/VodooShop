using System;
using CodeBase.Runtime.Core._Customer;
using UnityEngine;

namespace CodeBase.Runtime.Saves
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
            [SerializeField] public PlotCustomer[] PlotCustomers;
        }
    }
}
