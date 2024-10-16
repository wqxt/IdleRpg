using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private HealthObserver _healthObserver;
    [SerializeField] private PawnPool _pawnPool;

    private void Awake()
    {
        _pawnPool.Setup();
        SpawnCharacter();
        SpawnEnemy();
        SpawnHealthObserver();
    }


    public void SpawnHealthObserver() => _healthObserver = Instantiate(_healthObserver, transform.position, new Quaternion(0, 0, 0, 0), transform);

    private void SpawnCharacter()
    {
        Pawn characterPawn = Instantiate(_pawnPool.Character, _characterTransform.transform.position, new Quaternion(0, 0, 0, 0), transform);
        _pawnPool.ScenePawnList.Add(characterPawn);

        PawnHealth healthModel = new PawnHealth(characterPawn, this);
        _pawnPool.PawnHealthList.Add(healthModel);

    }

    private void SpawnEnemy()
    {
        float cumulativeChance = 0f;
        float randomValue = Random.Range(1f, 100f);

        for (int i = 0; i < _pawnPool.EnemyPool.Length; i++)
        {
            cumulativeChance += _pawnPool.EnemyPool[i].PawnConfiguration.SpawnChance;
            if (randomValue <= cumulativeChance)
            {
                Pawn enemyPawn = Instantiate(_pawnPool.EnemyPool[i], _enemyTransform.transform.position, new Quaternion(0, 0, 0, 0), transform);
                _pawnPool.ScenePawnList.Add(enemyPawn);

                PawnHealth healthModel = new PawnHealth(enemyPawn, this);
                _pawnPool.PawnHealthList.Add(healthModel);
                break;

            }
        }
    }

    public void RemovePawn(string pawnType)
    {
        for (int i = 0; i < _pawnPool.ScenePawnList.Count; i++)
        {
            if (_pawnPool.ScenePawnList[i].PawnConfiguration.Type == pawnType && _pawnPool.ScenePawnList[i].PawnConfiguration.Type != "Character")
            {

                for (int j = 0; j < _pawnPool.PawnHealthList.Count; j++)
                {
                    if (_pawnPool.PawnHealthList[j]._pawn.PawnConfiguration.Type == _pawnPool.ScenePawnList[i].PawnConfiguration.Type)
                    {
                        _pawnPool.PawnHealthList.RemoveAt(j);
                    }
                }

                _pawnPool.ScenePawnList[i].gameObject.SetActive(false);
                Destroy(_pawnPool.ScenePawnList[i].gameObject);
                _pawnPool.ScenePawnList.RemoveAt(i);

                SpawnEnemy();
            }
        }
    }
}