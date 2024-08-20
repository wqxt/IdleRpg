using UnityEngine;

namespace _game.StateMachine
{
    public class EntryState : State
    {
        public EntryState(Pawn player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            Debug.Log($"Current pawn = {_pawn.Configuration.name} enter in the current state = {this} ");

            _stateMachine.ChangeState(_pawn._prepareAttackState);
        }

        public override void Exit() => base.Exit();
    }
}