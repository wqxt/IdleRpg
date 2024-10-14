using UnityEngine;

public class HealthObserver : MonoBehaviour
{
    [SerializeField] private HealthView[] _healthview;
    [SerializeField] private PawnPool _pawnPool;

    private void Start()
    {
        SetupViews();
        SetupSubscribe();
    }

    private void SetupSubscribe()
    {
        foreach (Pawn pawn in _pawnPool.ScenePawnList)
        {
            pawn._attackState.Attacked += TakeDamage;
        }

        foreach (var healthModel in _pawnPool.PawnHealthList)
        {
            healthModel.PawnDeath += PawnDeath;
            healthModel.ChangeHealth += ChangeHealthView;
            healthModel.PawnHealthRemove += EnemyPawnHealthUnSubscribe;
        }
    }

    public void SetupViews()
    {
        foreach (var view in _healthview)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag)
                {
                    view.SetupHealth(pawn.PawnConfiguration.StartHealthValue);
                }
            }
        }
    }

    private void PawnDeath(string pawnType)
    {
        EnemyPawnHealthSubscribe(pawnType);
        SetupNewEnemyPawnView();
    }

    public void EnemyPawnHealthSubscribe(string pawnType)
    {
        foreach (var healthModel in _pawnPool.PawnHealthList)
        {
            if (healthModel._pawn.PawnConfiguration.Type == pawnType)
            {
                healthModel.PawnDeath += PawnDeath;
                healthModel.ChangeHealth += ChangeHealthView;
                healthModel._pawn._attackState.Attacked += TakeDamage;
                healthModel.PawnHealthRemove += EnemyPawnHealthUnSubscribe;
            }
        }
    }

    private void EnemyPawnHealthUnSubscribe(PawnHealth health)
    {
        health.PawnDeath -= PawnDeath;
        health.ChangeHealth -= ChangeHealthView;
        health._pawn._attackState.Attacked -= TakeDamage;
        health.PawnHealthRemove -= EnemyPawnHealthUnSubscribe;
    }


    public void SetupNewEnemyPawnView()
    {
        foreach (var view in _healthview)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag && pawn.PawnConfiguration.Type != "Character")
                {
                    view.SetupHealth(pawn.PawnConfiguration.StartHealthValue);
                }
            }
        }
    }

    private void TakeDamage(int damage, string pawnType)
    {
        for (int i = 0; i < _pawnPool.PawnHealthList.Count; i++)
        {
            var healthModel = _pawnPool.PawnHealthList[i];
            healthModel.TakeDamage(damage, pawnType);
        }

    }

    private void ChangeHealthView(int currentHealth, string pawnType)
    {
        foreach (var healthView in _healthview)
        {
            if (pawnType.Equals(healthView.tag))
            {
                healthView.ChangeHealth(currentHealth);
            }
        }
    }

    public void HealCharacter() //unity button
    {
        foreach (var view in _healthview)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag && view.tag == "Character")
                {
                    pawn.PawnConfiguration.CurrentHealthValue = pawn.PawnConfiguration.MaxHealthValue;
                    view.ChangeHealth(pawn.PawnConfiguration.MaxHealthValue);
                }
            }
        }
    }
}