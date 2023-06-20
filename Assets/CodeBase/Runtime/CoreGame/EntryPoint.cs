using UnityEngine;
using VContainer;

namespace CodeBase.Runtime.CoreGame
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private LevelBootstrapState _levelBootstrapState;

        public void Start()
        {
            _levelBootstrapState.Enter();
        }
    }
}