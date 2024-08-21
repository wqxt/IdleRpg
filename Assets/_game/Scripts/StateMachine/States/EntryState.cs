namespace _game.StateMachine
{
    public class EntryState : State
    {
        public EntryState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter() => _stateMachine.ChangeState(_pawn._prepareAttackState);

        public override void Exit() => base.Exit();
    }
}