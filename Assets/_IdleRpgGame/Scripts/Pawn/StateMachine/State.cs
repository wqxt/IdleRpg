namespace IdleGame.StateMachine
{
    public abstract class State
    {
        private protected StateMachine _stateMachine;
        private protected Pawn _pawn;
        public State(Pawn pawn, StateMachine stateMachine)
        {
            _pawn = pawn;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void LateUpdate() { }
    }
}