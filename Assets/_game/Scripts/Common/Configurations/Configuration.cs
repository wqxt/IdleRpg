using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObject/Configuration")]
public class Configuration : ScriptableObject
{
    [SerializeField] private string _configurationName;
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;

    [SerializeField] private float _armorValue;
    [SerializeField] private float _damageValue;
    [SerializeField, Range(0.1f, 5)] private float _attackTime;
    [SerializeField, Range(0.1f, 5)] private float _prepareAttackTime;
    [SerializeField] private bool meleeAttack;

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
