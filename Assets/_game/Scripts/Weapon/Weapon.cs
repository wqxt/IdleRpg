using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private string _type;
}
