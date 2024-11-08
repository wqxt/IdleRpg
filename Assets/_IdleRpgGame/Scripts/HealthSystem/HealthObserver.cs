using Zenject;

public class HealthObserver
{
    private HealthView [] _healthView;
    private PawnPool _pawnPool;
    private Spawner _spawner;

    [Inject]
    public void Construct(Spawner spawner, PawnPool pawnPool, HealthView[] healthView)
    {
        _pawnPool = pawnPool;
        _spawner = spawner;
        _healthView = healthView;
        Start();
    }

    private void Start()
    {
        SetupView();
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
        }
    }

    public void SetupView()
    {
        foreach (var view in _healthView)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag)
                {
                    view.RefreshHealth(pawn.PawnConfiguration.StartHealthValue);
                }
            }
        }
    }

    private void PawnDeath(string pawnType)
    {
        foreach (var healthModel in _pawnPool.PawnHealthList)
        {
            if (healthModel._pawn.PawnConfiguration.Type == pawnType)
            {
                PawnUnSubscribe(pawnType); // Отписываемся от событий пешку, которую будем скрывать
                break;
            }
        }

        _spawner.RemovePawn(pawnType); // Cкрываем пешку, создаем новую
        PawnSubscribe(pawnType); // Подписываем новую пешку 
        SetupNewPawnView(); // Обновляем health view 
    }

    public void PawnSubscribe(string pawnType)
    {
        foreach (var healthModel in _pawnPool.PawnHealthList)
        {
            if (healthModel._pawn.PawnConfiguration.Type == pawnType)
            {
                healthModel.PawnDeath += PawnDeath;
                healthModel.ChangeHealth += ChangeHealthView;
                healthModel._pawn._attackState.Attacked += TakeDamage;
            }
        }
    }

    private void PawnUnSubscribe(string pawnType)
    {
        foreach (var healthModel in _pawnPool.PawnHealthList)
        {
            if (healthModel._pawn.PawnConfiguration.Type == pawnType)
            {
                healthModel.PawnDeath -= PawnDeath;
                healthModel.ChangeHealth -= ChangeHealthView;
                healthModel._pawn._attackState.Attacked -= TakeDamage;
            }
        }
    }

    public void SetupNewPawnView()
    {
        foreach (var view in _healthView)
        {
            foreach (var pawn in _pawnPool.ScenePawnList)
            {
                if (pawn.PawnConfiguration.Type == view.tag && pawn.PawnConfiguration.Type != "Character")
                {
                    view.RefreshHealth(pawn.PawnConfiguration.StartHealthValue);
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
        foreach (var healthView in _healthView)
        {
            if (pawnType.Equals(healthView.tag))
            {
                healthView.ChangeHealth(currentHealth);
            }
        }
    }

    //unity button
    public void HealCharacter()
    {
        foreach (var view in _healthView)
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