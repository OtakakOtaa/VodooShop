using System;
using UnityEngine;

namespace CodeBase.Runtime.Core
{
    [Serializable] public sealed class Dialogue 
    {
        [SerializeField] private string _speak;
        [SerializeField] private string _prompt;

        public Dialogue(string speak, string prompt)
        {
            _speak = speak;
            _prompt = prompt;
        }
            
        public string Speak => _speak;
        public string Prompt => _prompt;
    }
}