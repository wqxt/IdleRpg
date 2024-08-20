using UnityEngine;

namespace _game.StateMachine
{
    public class PrepareAttackState : State
    {
        private float currentAnimationtime;
        private float animationLength;
        public PrepareAttackState(Pawn player, StateMachine stateMachine) : base(player, stateMachine) { }

        public override void Enter()
        {
            AnimationClip clip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];
            animationLength = clip.length;

            currentAnimationtime = _pawn.Configuration.PrepareAttackTime;
            _pawn._fightIndicatorAnimator.speed = animationLength / _pawn.Configuration.PrepareAttackTime;
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
                _stateMachine.ChangeState(_pawn._attackState);
            }
        }

        public override void Exit() => _pawn._fightIndicatorAnimator.SetBool("Play", false);
    }
}