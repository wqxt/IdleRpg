using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _damageValue;
    [SerializeField] private string _type;

    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }
    public int DamageValue
    {
        get { return _damageValue; }
        set { _damageValue = value; }
    }
}
