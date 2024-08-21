using System;
using UnityEngine;

namespace _game.StateMachine
{
    public class AttackState : State
    {
        private float currentAnimationTime;
        public AttackState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter()
        {
            currentAnimationTime = _pawn.Configuration.AttackTime;
            _pawn._attackSprite.gameObject.SetActive(true);

            AnimationClip fightIndicatorAnimatorClip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];
            AnimationClip pawnAnimatorClip = _pawn._pawnAnimator.runtimeAnimatorController.animationClips[0];

            _pawn._fightIndicatorAnimator.speed = fightIndicatorAnimatorClip.length / _pawn.Configuration.AttackTime;
            _pawn._pawnAnimator.speed = pawnAnimatorClip.length / _pawn.Configuration.AttackTime;

            _pawn._pawnAnimator.SetBool("Attack", true);
        }

        public override void Update()
        {
            if (currentAnimationTime > 0f)
            {
                currentAnimationTime -= Time.deltaTime;
            }
            else
            {
                _stateMachine.ChangeState(_pawn._entryState);
            }
        }
        public override void Exit()
        {
            _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);
            _pawn._pawnAnimator.SetBool("Attack", false);
            _pawn._attackSprite.gameObject.SetActive(false);
        }
    }
}