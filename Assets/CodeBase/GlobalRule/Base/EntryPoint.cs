using System;
using CodeBase.GlobalRule.Base.GlobalStateMachine.GameStates.States;
using CodeBase.GlobalRule.Base.GlobalStateMachine.StateMachine;
using UnityEngine;
using VContainer;

namespace CodeBase.GlobalRule.Base
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [Inject] private GlobalGameStateMachine _globalStateMachine;
        
        private void Start()
            => _globalStateMachine.Enter<Type, CurtainState>(typeof(MainMenuState));
    }
}