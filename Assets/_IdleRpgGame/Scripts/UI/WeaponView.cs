using UnityEngine;
using System;
using Zenject;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Weapon _meleeWeapon;
    [SerializeField] private Weapon _rangeWeapon;
    private WeaponObserver _weaponObserver;
    public event Action<Weapon, bool> OnWeaponSwitch;

    [Inject]
    private void Construct(WeaponObserver weaponObserver)
    {
        _weaponObserver = weaponObserver;
    }

    private void OnEnable() => OnWeaponSwitch += _weaponObserver.SetNewWeapon;

    private void OnDisable() => OnWeaponSwitch -= _weaponObserver.SetNewWeapon;

    // Unity Button
    public void MeleeWeapon() => SwitchWeapon(_meleeWeapon, true);

    // Unity Button
    public void RangeWeapon() => SwitchWeapon(_rangeWeapon, false);

    private void SwitchWeapon(Weapon weapon, bool isMelee) => OnWeaponSwitch?.Invoke(weapon, isMelee);
}
