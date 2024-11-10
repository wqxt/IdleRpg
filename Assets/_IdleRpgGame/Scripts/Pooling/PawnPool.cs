using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnConfiguration", menuName = "Scriptable Object/PawnPool")]
public class PawnPool : ScriptableObject
{
    [SerializeField] private protected Pawn[] EnemyPool;
    [SerializeField] internal protected Pawn Character;

    internal protected List<Pawn> ScenePawnList { get; set; }
    internal  List<PawnHealth> PawnHealthList { get; set; }
    internal protected List<Pawn> AvailableEnemies { get; set; }

    public void SetupPool()
    {
        ScenePawnList = new List<Pawn>();
        PawnHealthList = new List<PawnHealth>();
        AvailableEnemies = new List<Pawn>();

        // Создаем пешки и добавляем в пул текущих врагов на сцене 
        for (int i = 0; i < EnemyPool.Length; i++)
        {
            Pawn enemy = Instantiate(EnemyPool[i]);
            enemy.gameObject.SetActive(false);
            AvailableEnemies.Add(enemy);
        }
    }

    public Pawn GetEnemyFromPool()
    {
        if (AvailableEnemies.Count > 0)
        {
            var randomEnemy = Random.Range(0, EnemyPool.Length);
            Pawn enemy = AvailableEnemies[randomEnemy];
            AvailableEnemies.RemoveAt(randomEnemy);
            enemy.gameObject.SetActive(true);
            return enemy;
        }

        Pawn newEnemy = Instantiate(EnemyPool[Random.Range(0, EnemyPool.Length)]);
        return newEnemy;
    }

    public void ReturnEnemyToPool(Pawn enemy)
    {
        enemy.gameObject.SetActive(false);
        AvailableEnemies.Add(enemy);
    }
}