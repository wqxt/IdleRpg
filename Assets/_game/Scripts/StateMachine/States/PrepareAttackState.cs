using UnityEngine;

namespace _game.StateMachine
{
    public class PrepareAttackState : State
    {
        private float currentAnimationtime;
        public PrepareAttackState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter()
        {
            _pawn._prepareAttackSprite.gameObject.SetActive(true);
            currentAnimationtime = _pawn.Configuration.PrepareAttackTime;

            AnimationClip fightIndicatorAnimatorClip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];

            _pawn._fightIndicatorAnimator.speed = fightIndicatorAnimatorClip.length / _pawn.Configuration.PrepareAttackTime;
            _pawn._pawnAnimator.speed = 1f;
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

        public override void Exit()
        {
            _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);
            _pawn._prepareAttackSprite.gameObject.SetActive(false);
        }
    }
}