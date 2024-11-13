using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnConfiguration", menuName = "Scriptable Object/Configuration")]
public class PawnConfiguration : ScriptableObject
{
    [SerializeField] internal protected Weapon _currentWeapon;
    [SerializeField] internal protected Armor _currentArmor;
    [SerializeField] private string _type;
    [SerializeField] private int _startHealthValue;
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;
    [SerializeField] private int _minHealthValue;
    [SerializeField, Range(0.1f, 5)] private float _attackTime;
    [SerializeField, Range(0.1f, 5)] private float _prepareAttackTime;
    [SerializeField] private bool _meleeAttack;
    [SerializeField] private float _switchWeaponTime;
    [SerializeField] private int _currentAttackDamage;
    [SerializeField] private int _pawnDamage;

    public void EditorSetup()
    {
          _currentHealthValue = _startHealthValue;
          _maxHealthValue = _startHealthValue;
    }

    internal void Setup()
    {
        _startHealthValue = _maxHealthValue;
        _currentHealthValue = _maxHealthValue;
    }

    public int CurrentAttackDamage
    {
        get => _currentAttackDamage;
        set => _currentAttackDamage = value;
    }

    public int PawnDamage
    {
        get => _pawnDamage;
        set => _pawnDamage = value;
    }

    public string Type
    {
        get => _type;
        set => _type = value;
    }

    public int MaxHealthValue
    {
        get => _maxHealthValue;
        set => _maxHealthValue = value;
    }
    public bool MeleeAttack
    {
        get => _meleeAttack;
        set => _meleeAttack = value;
    }

    public int StartHealthValue
    {
        get => _startHealthValue;

        set
        {
            if (value >= _maxHealthValue)
            {
                _startHealthValue = _maxHealthValue;
            }
            else
            {
                _startHealthValue = value;
            }

        }
    }

    public int CurrentHealthValue
    {
        get => _currentHealthValue;

        set
        {
            if (value >= _maxHealthValue)
            {
                _currentHealthValue = _maxHealthValue;
            }


            if (value <= 0)
            {

                _currentHealthValue = 0;
            }
            else
            {
                _currentHealthValue = value;
            }
        }
    }


    public int MinHealthValue
    {
        get => _minHealthValue;

        set
        {
            if (value < 0)
            {
                _minHealthValue = 0;
            }

            else
            {
                _minHealthValue = value;
            }
        }
    }

    public float SwitchWeaponTime
    {
        get => _switchWeaponTime;

        set
        {
            if (value < 0)
            {
                _switchWeaponTime = 2f;
            }
            else
            {
                _switchWeaponTime = value;
            }
        }
    }
    public float AttackTime
    {
        get => _attackTime;

        set
        {
            if (value < 0)
            {
                _attackTime = 1f;
            }
            else
            {
                _attackTime = _currentWeapon.AttackSpeed;
            }
        }
    }
    public float PrepareAttackTime
    {
        get => _prepareAttackTime;

        set
        {
            if (value <= 0)
            {
                _prepareAttackTime = 1f;
            }
            else
            {
                _prepareAttackTime = value;
            }
        }
    }
}