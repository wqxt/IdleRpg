using UnityEngine;

public class SwitchWeaponCanvas : MonoBehaviour
{
    [SerializeField] private PawnConfiguration _characterConfiguration;
    [SerializeField] private Weapon _meleeWeapon;
    [SerializeField] private Weapon _rangeWeapon;

    //unit event
    public void MeleeWeapon()
    {
        _characterConfiguration.MeleeAttack = true;
        _characterConfiguration._currentWeapon = _meleeWeapon;
        _characterConfiguration.AttackTime = _characterConfiguration._currentWeapon.AttackSpeed;
        _characterConfiguration.CurrentAttackDamage = (int)_meleeWeapon.DamageValue + _characterConfiguration.PawnDamage;

    }
    public void RangeWeapon()
    {
        _characterConfiguration.MeleeAttack = false;
        _characterConfiguration._currentWeapon = _rangeWeapon;
        _characterConfiguration.AttackTime = _characterConfiguration._currentWeapon.AttackSpeed;
        _characterConfiguration.CurrentAttackDamage = (int)_rangeWeapon.DamageValue + _characterConfiguration.PawnDamage;

    }
}