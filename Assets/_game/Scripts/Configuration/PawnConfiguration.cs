using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObject/Configuration")]
public class PawnConfiguration : ScriptableObject
{
    [SerializeField] internal protected Weapon _currentWeapon;
    [SerializeField] internal protected Armor _currentArmor;
    [SerializeField] private string _configurationType;
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
    [SerializeField] private float _spawnChance;

    public int CurrentAttackDamage { get; set; }
    public string ConfigurationType
    {
        get
        {
            return _configurationType;
        }
        set
        {
            _configurationType = value;
        }
    }
    public int PawnDamage
    {
        get
        {
            return _pawnDamage;
        }
        set
        {
            _pawnDamage = value;
        }
    }
    public float SpawnChance
    {
        get
        {
            return _spawnChance;
        }

        set
        {
            _spawnChance = value;
        }

    }
    public int StartHealthValue
    {
        get
        {
            return _startHealthValue;
        }

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
    public int MinHealthValue
    {
        get
        {
            return _minHealthValue;
        }

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
    public int MaxHealthValue
    {
        get
        {
            return _maxHealthValue;
        }

        set
        {
            if (value > 100)
            {
                _maxHealthValue = 100;
            }
            else
            {
                _maxHealthValue = value;
            }
        }
    }
    public bool MeleeAttack
    {
        get
        {
            return _meleeAttack;
        }

        set
        {
            _meleeAttack = value;

        }
    }
    public int CurrentHealthValue
    {
        get
        {
            return _currentHealthValue;
        }

        set
        {
            if (value < 0)
            {
                _currentHealthValue = 0;
            }
            else
            {
                _currentHealthValue = value;
            }

            if (value > _maxHealthValue)
            {
                _currentHealthValue = _maxHealthValue;
            }
        }
    }
    public float SwitchWeaponTime
    {
        get
        {
            return _switchWeaponTime;
        }

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
        get
        {
            return _attackTime;
        }

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
        get
        {
            return _prepareAttackTime;
        }

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