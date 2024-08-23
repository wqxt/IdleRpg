using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObject/Armor")]
public class Armor : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private string _type;
}