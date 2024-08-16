using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfiguration", menuName = "ScriptableObject/CharacterConfiguration")]

public class EnemyConfiguration : ScriptableObject
{
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;

    [SerializeField] private float _armorValue;
    [SerializeField] private float _damageValue;
}
