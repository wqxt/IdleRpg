using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObject/Configuration")]
public class Configuration : ScriptableObject
{
    [SerializeField] private string _configurationName;
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;

    [SerializeField] private float _armorValue;
    [SerializeField] private float _damageValue;
    [SerializeField] private float _attackTime;
    [SerializeField] private float _prepareAttackTime;
    [SerializeField] private bool meleeAttack;

    public float AttackTime
    {
        get 
        {
            return _attackTime;
        }

        set
        {
            if (value <= 0)
            {
                return ;
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
                return;
            }
            else
            {
                _prepareAttackTime = value;
            }
        }
    }
}
