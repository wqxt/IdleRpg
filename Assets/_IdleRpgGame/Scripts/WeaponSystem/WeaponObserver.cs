using UnityEngine;

public class WeaponObserver
{
    private readonly PawnConfiguration _characterConfiguration;

    public WeaponObserver(PawnConfiguration characterConfiguration)
    {
        _characterConfiguration = characterConfiguration;
    }

    public void SetNewWeapon(Weapon weapon, bool isMelee)
    {
        _characterConfiguration.MeleeAttack = isMelee;
        _characterConfiguration._currentWeapon = weapon;
        _characterConfiguration.AttackTime = weapon.AttackSpeed;
        _characterConfiguration.CurrentAttackDamage = (int)weapon.DamageValue + _characterConfiguration.PawnDamage;
    }
}