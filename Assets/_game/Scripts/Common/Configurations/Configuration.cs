using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObject/Configuration")]
public class Configuration : ScriptableObject
{
    [SerializeField] private string _configurationName;
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;
    [SerializeField, Range(0.1f, 5)] private float _attackTime;
    [SerializeField, Range(0.1f, 5)] private float _prepareAttackTime;
    [SerializeField] private bool _meleeAttack;
    [SerializeField] private float _switchWeaponTime;

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
                _switchWeaponTime = 1f;
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
                _attackTime = value;
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
