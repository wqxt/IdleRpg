using System;
using UnityEngine;

public class PawnHealth
{
    internal protected readonly Pawn _pawn;
    private readonly Spawner _spawner;

    public event Action<PawnHealth> PawnHealthRemove;
    public event Action<int, string> ChangeHealth;
    public event Action<string> PawnDeath;

    public PawnHealth(Pawn pawn, Spawner spawner)
    {
        _pawn = pawn;
        _spawner = spawner;
    }

    public void TakeDamage(int damage, string pawnType)
    {
        if (_pawn.PawnConfiguration.Type != pawnType)
        {
            _pawn.PawnConfiguration.CurrentHealthValue -= damage;
            ChangeHealth?.Invoke(_pawn.PawnConfiguration.CurrentHealthValue, _pawn.PawnConfiguration.Type);

            if (_pawn.PawnConfiguration.CurrentHealthValue <= 0)
            {
                Death(_pawn.PawnConfiguration.Type);
            }
        }
    }

    private void Death(string pawnType)
    {
        Debug.Log($"Pawn = {_pawn.PawnConfiguration.Type} is Death");

        if (pawnType.Equals("Character"))
        {
            IdleGameState.CurrentState = GameState.EntryState;
        }
        else
        {
            _spawner.RemovePawn(pawnType);
            PawnDeath?.Invoke(pawnType);
            PawnHealthRemove?.Invoke(this);
        }
    }
}