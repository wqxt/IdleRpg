using Zenject;

public class HealthObserver
{
    public HealthView[] _healthView;
    private PawnPool _pawnPool;
    private Spawner _spawner;

    [Inject]
    public void Construct(Spawner spawner, PawnPool pawnPool, HealthView[] healthView)
    {
        _pawnPool = pawnPool;
        _spawner = spawner;
        _healthView = healthView;
        Initialization();
    }

    private void Initialization()
    {
        SetupPawnView();
        SetupSubscribe();
    }

    private void SetupSubscribe()
    {
        foreach (Pawn pawn in _pawnPool.ScenePawnList)
        {
            pawn._attackState.Attacked += PawnTakeDamage;
        }

        foreach (var pawnHealth in _pawnPool.PawnHealthList)
        {
            pawnHealth.PawnDeath += PawnDeath;
            pawnHealth.ChangeHealth += UpdatePawnHealthView;
        }
    }

    public void SetupPawnView()
    {
        foreach (var view in _healthView)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag && IdleGameState.CurrentState == GameState.EntryState)
                {
                    view.SetupHealth(pawn.PawnConfiguration.StartHealthValue);
                }
                else if(pawn.PawnConfiguration.Type == view.tag && pawn.PawnConfiguration.Type != "Character")
                {
                    view.UpdateHealth(pawn.PawnConfiguration.CurrentHealthValue);
                }
            }
        }
    }

    private void PawnDeath(string pawnType)
    {
        foreach (var pawnHealth in _pawnPool.PawnHealthList)
        {
            if (pawnHealth._pawn.PawnConfiguration.Type == pawnType)
            {
                PawnUnSubscribe(pawnType); // Отписываемся от событий пешку, которую будем скрывать
                break;
            }
        }

        _spawner.RemovePawn(pawnType); // Cкрываем пешку, создаем новую
        PawnSubscribe(pawnType); // Подписываем новую пешку 
        SetupPawnView(); // Настроиваем отображение здоровья
    }

    public void PawnSubscribe(string pawnType)
    {
        foreach (var pawnhealth in _pawnPool.PawnHealthList)
        {
            if (pawnhealth._pawn.PawnConfiguration.Type == pawnType)
            {
                pawnhealth.PawnDeath += PawnDeath;
                pawnhealth.ChangeHealth += UpdatePawnHealthView;
                pawnhealth._pawn._attackState.Attacked += PawnTakeDamage;
            }
        }
    }

    private void PawnUnSubscribe(string pawnType)
    {
        foreach (var pawnHealth in _pawnPool.PawnHealthList)
        {
            if (pawnHealth._pawn.PawnConfiguration.Type == pawnType)
            {
                pawnHealth.PawnDeath -= PawnDeath;
                pawnHealth.ChangeHealth -= UpdatePawnHealthView;
                pawnHealth._pawn._attackState.Attacked -= PawnTakeDamage;
            }
        }
    }

    private void PawnTakeDamage(int damage, string pawnType)
    {
        for (int i = 0; i < _pawnPool.PawnHealthList.Count; i++)
        {
            var pawnHealth = _pawnPool.PawnHealthList[i];
            pawnHealth.TakeDamage(damage, pawnType);
   
        }
    }

    public void UpdatePawnHealthView(int currentHealth, string pawnType)
    {

        foreach (var healthView in _healthView)
        {
            if (pawnType.Equals(healthView.tag))
            {
                healthView.UpdateHealth(currentHealth);
            }
        }
    }
}