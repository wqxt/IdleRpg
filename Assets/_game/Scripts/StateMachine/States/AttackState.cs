using UnityEngine;

namespace _game.StateMachine
{
    public class AttackState : State
    {
        private float currentAnimationtime;
        private float animationLength;
        public AttackState(Pawn player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            AnimationClip clip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];
            animationLength = clip.length;

            currentAnimationtime = _pawn.Configuration.AttackTime;
            _pawn._fightIndicatorAnimator.speed = animationLength / _pawn.Configuration.AttackTime;
            _pawn._pawnAnimator.speed = animationLength / _pawn.Configuration.AttackTime;

            _pawn._pawnAnimator.SetBool("Attack", true);
            _pawn._fightIndicatorAnimator.Play("StartIndicator");

        }
        public override void Update()
        {
            if (currentAnimationtime > 0f)
            {
                currentAnimationtime -= Time.deltaTime;
            }
            else
            {
                _stateMachine.ChangeState(_pawn._prepareAttackState);
            }
        }
        public override void Exit()
        {
            _pawn._pawnAnimator.speed = 1f;
            _pawn._pawnAnimator.SetBool("Attack", false);
        }
    }
}