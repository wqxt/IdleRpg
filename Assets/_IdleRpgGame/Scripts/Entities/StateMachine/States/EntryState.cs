using UnityEngine;

namespace IdleGame.StateMachine
{
    public class EntryState : State
    {
        public EntryState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter()
        {
            _pawn._fightIndicatorAnimator.speed = 0;
            _pawn._pawnAnimator.speed = 0;
        }

        public override void LateUpdate()
        {
            if (IdleGameState.CurrentState == GameState.EntryState)
            {
                _pawn._fightIndicatorAnimator.speed = 0;
                _pawn._pawnAnimator.speed = 0;
            }
            else
            {
                _stateMachine.ChangeState(_pawn._prepareAttackState);
            }
        }

        public override void Exit() { }
    }
}