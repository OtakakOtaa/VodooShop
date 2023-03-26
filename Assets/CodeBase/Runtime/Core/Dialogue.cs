using System;
using UnityEngine;

namespace CodeBase.Runtime.Core
{
    [Serializable] public sealed class Dialogue 
    {
        [SerializeField] private string _speak;
        [SerializeField] private string _prompt;

        public Dialogue(string speak)
            => _speak = speak;

        public string Speak => _speak;
        public string Prompt => _prompt;
    }
}