using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PawnConfiguration", menuName = "Scriptable Object/PawnPool")]
public class PawnPool : ScriptableObject
{
    [SerializeField] private protected Pawn[] _enemyPool;
    [SerializeField] internal protected Pawn _character;

    internal protected List<Pawn> ScenePawnList { get; set; }
    internal List<PawnHealth> PawnHealthList { get; set; }
    internal protected List<Pawn> AvailableEnemies { get; set; }

    private IPawnFactory _pawnFactory;

    [Inject]
    public void Construct(IPawnFactory pawnFactory)
    {
        _pawnFactory = pawnFactory;
    }

    public void SetupPool()
    {
        ScenePawnList = new List<Pawn>();
        PawnHealthList = new List<PawnHealth>();
        AvailableEnemies = new List<Pawn>();


        foreach (var enemyPrefab in _enemyPool)
        {
            Pawn enemy = _pawnFactory.CreatePawn(enemyPrefab);
            enemy.gameObject.SetActive(false);
            AvailableEnemies.Add(enemy);
        }
    }

    public Pawn GetCharacter()
    {
        Pawn newCharacter = _pawnFactory.CreatePawn(_character);
        return newCharacter;
    }

    public Pawn GetEnemyFromPool()
    {
        if (AvailableEnemies.Count > 0)
        {
            var randomEnemy = Random.Range(0, _enemyPool.Length);
            Pawn enemy = AvailableEnemies[randomEnemy];
            AvailableEnemies.RemoveAt(randomEnemy);
            enemy.gameObject.SetActive(true);
            return enemy;
        }

        Pawn newEnemy = _pawnFactory.CreatePawn(_enemyPool[Random.Range(0, _enemyPool.Length)]);
        return newEnemy;
    }

    public void ReturnEnemyToPool(Pawn enemy)
    {
        enemy.gameObject.SetActive(false);
        AvailableEnemies.Add(enemy);
    }
}