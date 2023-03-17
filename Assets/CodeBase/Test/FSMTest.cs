using System;
using CodeBase.Runtime.Infrastructure;
using CodeBase.Runtime.Infrastructure.FSM;
using CodeBase.Runtime.Infrastructure.FSM.States;
using NUnit.Framework;

namespace CodeBase.Test
{
    public class FSMTest
    {
        #region SetUp

        private const string ExpectedMessage = "Success";
        private const string ExpectedPayload = "payload";

        private FinalGameStateMachine _finalGameStateMachine;

        private SimpleState _simpleState;
        private ExitableState _exitableState;
        private StateWithPayload _stateWithPayload;

        [SetUp]
        public void InitStates()
        {
            _simpleState = new SimpleState();
            _exitableState = new ExitableState();
            _stateWithPayload = new StateWithPayload();

            _finalGameStateMachine = new FinalGameStateMachine(
                new State[] { _simpleState, _exitableState, _stateWithPayload }
            );
        }

        #endregion

        [TearDown]
        public void ClearFSM()
        {
            _finalGameStateMachine = new FinalGameStateMachine(
                new State[] { _simpleState, _exitableState, _stateWithPayload });
        }

        [Test]
        public void SimpleEnterTransitionTest()
        {
            Assert.Catch(() => { _finalGameStateMachine.Enter<SimpleState>(); });
        }

        [Test]
        public void TransactionWithExitTest()
        {
            // Prepare
            try
            {
                _finalGameStateMachine.Enter<ExitableState>();
            }
            catch (Exception _)
            {
                /* ignored*/
            }

            // Assert
            Assert.Catch(() => { _finalGameStateMachine.Enter<SimpleState>(); });
        }

        [Test]
        public void TransactionWithPayloadTest()
        {
            Assert.Catch(() => { _finalGameStateMachine.Enter<string, StateWithPayload>(ExpectedPayload); });
        }

        #region Resources

        private class SimpleState : State
        {
            public override void Enter()
                => throw new Exception(ExpectedMessage);
        }

        private class ExitableState : State, IExitableState
        {
            public override void Enter()
            {
            }

            public void Exit()
                => throw new Exception(ExpectedMessage);
        }

        private class StateWithPayload : StateWithPayload<string>
        {
            public override void Enter(string payload)
            {
                if (payload is ExpectedPayload)
                    throw new Exception(ExpectedMessage);
            }
        }

        #endregion
    }
}