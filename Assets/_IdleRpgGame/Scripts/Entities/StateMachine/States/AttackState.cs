using System;
using UnityEngine;

namespace IdleGame.StateMachine
{
    public class AttackState : State
    {
        private float _currentAnimationTime;
        private bool _meleeAttack;
        
        public event Action<int,string> Attacked;
        public AttackState(Pawn pawn, StateMachine stateMachine) : base(pawn, stateMachine) { }

        public override void Enter()
        {
            _meleeAttack = _pawn.PawnConfiguration.MeleeAttack;

            _currentAnimationTime = _pawn.PawnConfiguration.AttackTime;
            _pawn._attackSprite.gameObject.SetActive(true);

            AnimationClip fightIndicatorAnimatorClip = _pawn._fightIndicatorAnimator.runtimeAnimatorController.animationClips[0];
            AnimationClip pawnAnimatorClip = _pawn._pawnAnimator.runtimeAnimatorController.animationClips[0];

            _pawn._fightIndicatorAnimator.speed = fightIndicatorAnimatorClip.length / _pawn.PawnConfiguration.AttackTime;
            _pawn._pawnAnimator.speed = pawnAnimatorClip.length / _pawn.PawnConfiguration.AttackTime;


            if (_meleeAttack)
            {
                _pawn._pawnAnimator.SetBool("MeleeAttack", true);
            }
            else
            {
                _pawn._pawnAnimator.SetBool("RangeAttack", true);
            }
        }

        public override void Update()
        {
            if (_currentAnimationTime > 0f && IdleGameState.CurrentState == GameState.FightState)
            {
                _currentAnimationTime -= Time.deltaTime;
            }
            else
            {
                if(_meleeAttack != _pawn.PawnConfiguration.MeleeAttack)
                {
                    _stateMachine.ChangeState(_pawn._switchWeaponState);
                }
                else
                {
                    _stateMachine.ChangeState(_pawn._entryState);
                }
            }
        }

        public override void Exit()
        {

            Attacked?.Invoke(_pawn.PawnConfiguration.CurrentAttackDamage,_pawn.PawnConfiguration.Type);

            _pawn._fightIndicatorAnimator.Play("Indicator", 0, 0f);

            if (_meleeAttack)
            {
                _pawn._pawnAnimator.SetBool("MeleeAttack", false);
            }
            else
            {

                _pawn._pawnAnimator.SetBool("RangeAttack", false);
            }

            _pawn._attackSprite.gameObject.SetActive(false);
        }
    }
}