using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObject/Armor")]
public class Armor : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _armorValue;
    [SerializeField] private string _type;

    public int ArmorValue
    {
        get { return _armorValue; }
        set { _armorValue = value; }
    }
}