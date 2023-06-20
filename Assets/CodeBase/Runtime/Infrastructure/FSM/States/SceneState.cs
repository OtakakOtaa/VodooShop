using System;
using UnityEngine;

namespace CodeBase.Runtime.Infrastructure.FSM.States
{
    [Serializable] public abstract class StateWithOwnScene : IState
    {
        [SerializeField, ApplicationScene] private string _sceneTag;

        protected StateWithOwnScene(string sceneTag)
        {
            _sceneTag = sceneTag;
        }

        public string SceneTag => _sceneTag;
        public abstract void Enter();
    }
}