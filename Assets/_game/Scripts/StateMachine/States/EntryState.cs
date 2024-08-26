using UnityEngine;

namespace _game.StateMachine
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
            Debug.Log($"Current pawn = {_pawn.PawnConfiguration.name}");
            if (_pawn.GameConfiguration.CurrentState == GameState.EntryState )
            {
                _pawn._fightIndicatorAnimator.speed = 0;
                _pawn._pawnAnimator.speed = 0;
                _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);
                _pawn._fightIndicatorAnimator.Play("RangeAttack", 0, 0f);
                _pawn._fightIndicatorAnimator.Play("MeleeAttack", 0, 0f);
            }
            else
            {
                _stateMachine.ChangeState(_pawn._prepareAttackState);
            }
        }

        public override void Exit() { }
    }
}