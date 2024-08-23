using UnityEngine;

public class SwitchWeaponCanvas : MonoBehaviour
{
    public Configuration _characterConfiguration;

    public void MeleeAttack() => _characterConfiguration.MeleeAttack = true;
    public void RangeAttack() => _characterConfiguration.MeleeAttack = false;
}