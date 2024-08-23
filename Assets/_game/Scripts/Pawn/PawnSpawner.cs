using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private Pawn[] _enemyPool;
    [SerializeField] private GameObject _character;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Transform _characterTransform;

    private void Awake()
    {
        SpawnEnemy();
        SpawnCharacter();
    }

    private void SpawnEnemy()
    {
        System.Random randomEnemyPawn = new System.Random();
        var tileValue = randomEnemyPawn.Next(_enemyPool.Length);

        Pawn enemy = Instantiate(_enemyPool[tileValue], _enemyTransform.transform.position, Quaternion.identity);
    }

    private void SpawnCharacter()
    {
       GameObject character = Instantiate(_character, _characterTransform.transform.position, Quaternion.identity);
    }
}
