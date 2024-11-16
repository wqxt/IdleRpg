using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PawnPool _pawnPool;

    [Inject]
    public void Construct(PawnPool pawnPool)
    {
        _pawnPool = pawnPool;
    }

    private void Awake()
    {
        _pawnPool.SetupPool();
        SpawnCharacter();
        SpawnEnemy();
    }

    private void SpawnCharacter()
    {
        Pawn characterPawn = _pawnPool.GetCharacter();
        characterPawn.transform.position = characterPawn._pawnTransform.position;
        _pawnPool.ScenePawnList.Add(characterPawn);

        PawnHealth pawnHealth = new PawnHealth(characterPawn);
        _pawnPool.PawnHealthList.Add(pawnHealth);
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

        PawnHealth pawnHealth = new PawnHealth(enemyPawn);
        _pawnPool.PawnHealthList.Add(pawnHealth);
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