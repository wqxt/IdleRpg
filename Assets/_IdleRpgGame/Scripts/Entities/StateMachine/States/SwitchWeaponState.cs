using UnityEngine;

namespace IdleGame.StateMachine
{
    public class SwitchWeaponState : State
    {
        private float _currentAnimationTime;
        public SwitchWeaponState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine)
        {
        }

        public override void Enter()
        {
            _pawn._switchWeaponSprite.gameObject.SetActive(true);
            _currentAnimationTime = _pawn.PawnConfiguration.SwitchWeaponTime;

            AnimationClip fightIndicatorAnimatorClip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];

            _pawn._fightIndicatorAnimator.speed = fightIndicatorAnimatorClip.length / _pawn.PawnConfiguration.SwitchWeaponTime;
            _pawn._pawnAnimator.speed = 1f;
        }

        public override void Update()
        {
            if (_currentAnimationTime > 0f && IdleGameState.CurrentState == GameState.FightState)
            {
                _currentAnimationTime -= Time.deltaTime;
            }
            else
            {
                _stateMachine.ChangeState(_pawn._prepareAttackState);
            }
        }

        public override void Exit()
        {
            _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);
            _pawn._switchWeaponSprite.gameObject.SetActive(false);
        }
    }
}