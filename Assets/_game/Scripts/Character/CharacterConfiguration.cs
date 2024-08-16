using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfiguration", menuName = "ScriptableObject/CharacterConfiguration")]
public class CharacterConfiguration : ScriptableObject
{
    [SerializeField] private int _currentHealthValue;
    [SerializeField] private int _maxHealthValue;

    [SerializeField] private float _armorValue;
    [SerializeField] private float _damageValue;
    [SerializeField] private float _attackTIme;
}
