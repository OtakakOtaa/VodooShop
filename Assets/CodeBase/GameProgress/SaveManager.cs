using System;
using CodeBase.Customers.Data;
using UnityEngine;

namespace CodeBase.GameProgress
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
