using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawner : MonoBehaviour
{
    [SerializeField] private Pawn[] _enemyPool;
    [SerializeField] private Pawn _character;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private List<Pawn> _scenePawnList;
    [SerializeField] private GameConfiguration GameConfiguration;
    private static PawnSpawner _instance;

    public List<Pawn> ScenePawnList
    {
        get
        {
            return _scenePawnList;
        }
    }

    public static PawnSpawner Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        SpawnCharacterPawn();
        SpawnEnemyPawn();
    }

    private void SpawnCharacterPawn()
    {
        Pawn characterPawn = Instantiate(_character, _characterTransform.transform.position, Quaternion.identity);
        _scenePawnList.Add(characterPawn);
    }

    private void SpawnEnemyPawn()
    {
        float randomValue = Random.Range(1f, 100f);

        for (int i = 0; i < _enemyPool.Length ; i++)
        {
            if (_enemyPool[i].PawnConfiguration.SpawnChance <= randomValue)
            {
                Pawn enemyPawn = Instantiate(_enemyPool[i], _enemyTransform.transform.position, Quaternion.identity);
                _scenePawnList.Add(enemyPawn);
                break;
            }
        }
    }

    public void DeletePawn(Pawn pawn)
    {

        if (pawn.PawnConfiguration.ConfigurationType == _scenePawnList[0].PawnConfiguration.ConfigurationType) 
        {
            for (int i = 0; i < _scenePawnList.Count; i++)
            {
                _scenePawnList[i].GameConfiguration.CurrentState = GameState.EntryState;
            }
        }
        else
        {   
            for (int i = 0; i < _scenePawnList.Count; i++)
            {
                if (_scenePawnList[i].PawnConfiguration.ConfigurationType == pawn.PawnConfiguration.ConfigurationType)
                {

                    _scenePawnList[i].gameObject.SetActive(false);
                    Destroy(_scenePawnList[i].gameObject);
                    _scenePawnList.RemoveAt(i);

                    float cumulativeChance = 0f;
                    float randomValue = Random.Range(1f, 100f);

                    for (int j = 0; j < _enemyPool.Length; j++)
                    {
                        cumulativeChance += _enemyPool[j].PawnConfiguration.SpawnChance;
                        if (randomValue <= cumulativeChance)
                        {
                            Pawn enemyPawn = Instantiate(_enemyPool[j], _enemyTransform.transform.position, Quaternion.identity);
                            _scenePawnList.Add(enemyPawn);
                            break;
                        }
                    }
                }
            }
        }
    }
}