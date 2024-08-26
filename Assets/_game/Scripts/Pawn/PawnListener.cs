using UnityEngine;

public class PawnListener : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private PawnHealthCanvas _healthCanvas;

    private void OnEnable()
    {
        _pawn._attackState.Attacked += TakeDamage;
        _healthCanvas.PawnHealthEnded += Death;
    }

    private void OnDisable()
    {
        _pawn._attackState.Attacked -= TakeDamage;
        _healthCanvas.PawnHealthEnded += Death;
    }

    public void TakeDamage(int damage, Pawn pawn)
    {
        for (int i = 0; i < PawnSpawner.Instance.ScenePawnList.Count; i++)
        {

            if (PawnSpawner.Instance.ScenePawnList[i].PawnConfiguration.name != pawn.PawnConfiguration.name)
            {
                int damageAfterArmor = damage - PawnSpawner.Instance.ScenePawnList[i].PawnConfiguration._currentArmor.ArmorValue;
                if (damageAfterArmor >= 0)
                {
                    PawnSpawner.Instance.ScenePawnList[i].PawnConfiguration.CurrentHealthValue -= damageAfterArmor;
                }
                else
                {
                    break;
                }

                break;
            }
        }
    }

    public void Death() => PawnSpawner.Instance.DeletePawn(_pawn);
}