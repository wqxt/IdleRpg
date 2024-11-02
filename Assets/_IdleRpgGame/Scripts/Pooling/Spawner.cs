using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PawnPool _pawnPool;

    private void Awake()
    {
        _pawnPool.SetupPool();
        SpawnCharacter();
        SpawnEnemy();
    }

    private void SpawnCharacter()
    {
        Pawn characterPawn = Instantiate(_pawnPool.Character, new Vector3(0, 0, 0), Quaternion.identity);
        characterPawn.transform.position = characterPawn._pawnTransform.position;
        _pawnPool.ScenePawnList.Add(characterPawn);

        PawnHealth healthModel = new PawnHealth(characterPawn, this);
        _pawnPool.PawnHealthList.Add(healthModel);
    }

    private void SpawnEnemy()
    {
        Pawn enemyPawn = _pawnPool.GetEnemyFromPool();
        enemyPawn.transform.position = enemyPawn._pawnTransform.position;

        if (enemyPawn == null)
        {
            Debug.LogError("Enemy pawn is null!");
            return;
        }
        _pawnPool.ScenePawnList.Add(enemyPawn);

        PawnHealth healthModel = new PawnHealth(enemyPawn, this);
        _pawnPool.PawnHealthList.Add(healthModel);
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

                _pawnPool.ReturnEnemyToPool(_pawnPool.ScenePawnList[i]);
                _pawnPool.ScenePawnList.RemoveAt(i);

                SpawnEnemy();
            }
        }
    }
}