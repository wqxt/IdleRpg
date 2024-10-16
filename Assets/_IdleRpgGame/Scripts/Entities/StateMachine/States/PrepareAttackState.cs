using UnityEngine;

namespace IdleGame.StateMachine
{
    public class PrepareAttackState : State
    {
        private AnimationClip _fightIndicatorAnimatorClip;
        private float _animationNormalizedTime;
        private float _currentAnimationTime;
        private bool _meleeAttack;
        private bool _wasSwitch;

        public PrepareAttackState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter()
        {

            _meleeAttack = _pawn.PawnConfiguration.MeleeAttack;
            _pawn._prepareAttackSprite.gameObject.SetActive(true);


            if (_wasSwitch)
            {
                _wasSwitch = false;

                _pawn._fightIndicatorAnimator.speed = _fightIndicatorAnimatorClip.length / _pawn.PawnConfiguration.PrepareAttackTime;
                _pawn._fightIndicatorAnimator.Play("Indicator", 0, _animationNormalizedTime);

            }
            else
            {
                _currentAnimationTime = _pawn.PawnConfiguration.PrepareAttackTime;
                _fightIndicatorAnimatorClip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];

                _pawn._fightIndicatorAnimator.speed = _fightIndicatorAnimatorClip.length / _pawn.PawnConfiguration.PrepareAttackTime;
                _pawn._pawnAnimator.speed = 1f;
            }
        }

        public override void Update()
        {


            if (_meleeAttack != _pawn.PawnConfiguration.MeleeAttack)
            {
                AnimatorStateInfo fightAnimatorStateInfo = _pawn._fightIndicatorAnimator.GetCurrentAnimatorStateInfo(0);
                _animationNormalizedTime = fightAnimatorStateInfo.normalizedTime;

                _stateMachine.ChangeState(_pawn._switchWeaponState);
            }

            if (_currentAnimationTime > 0f )
            {
                _currentAnimationTime -= Time.deltaTime;
            }
            else
            {
                _stateMachine.ChangeState(_pawn._attackState);
            }

            if( IdleGameState.CurrentState != GameState.FightState)
            {
                _stateMachine.ChangeState(_pawn._entryState);
            }
        }

        public override void Exit()
        {
            _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);
            _pawn._prepareAttackSprite.gameObject.SetActive(false);

            if (_meleeAttack != _pawn.PawnConfiguration.MeleeAttack)
            {
                _wasSwitch = true;
            }
        }
    }
}