using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnPool", menuName = "Scriptable Object/PawnPool")]
public class PawnPool : ScriptableObject
{
    [SerializeField] internal protected Pawn[] EnemyPool;
    [SerializeField] internal protected Pawn Character;

    internal protected List<Pawn> ScenePawnList { get; set; }
    internal protected List<PawnHealth> PawnHealthList { get; set; } 

    public void Setup()
    {
        ScenePawnList = new List<Pawn>();
        PawnHealthList = new List<PawnHealth>();
    }
    
}