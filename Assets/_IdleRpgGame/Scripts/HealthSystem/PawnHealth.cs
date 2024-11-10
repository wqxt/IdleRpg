using System;
using UnityEngine;

public class PawnHealth
{
    internal protected readonly Pawn _pawn;

    public event Action<int, string> ChangeHealth;
    public event Action<string> PawnDeath;

    public PawnHealth(Pawn pawn)
    {
        _pawn = pawn;

    }

    public void TakeDamage(int damage, string pawnType)
    {
        if (_pawn.PawnConfiguration.Type != pawnType)
        {
            _pawn.PawnConfiguration.CurrentHealthValue -= damage;


            if (_pawn.PawnConfiguration.CurrentHealthValue <= 0)
            {
                Death(_pawn.PawnConfiguration.Type);
            }

            ChangeHealth?.Invoke(_pawn.PawnConfiguration.CurrentHealthValue, _pawn.PawnConfiguration.Type);
        }
    }

    private void Death(string pawnType)
    {

        if (pawnType.Equals("Character"))
        {
            IdleGameState.CurrentState = GameState.EntryState;
        }

        PawnDeath?.Invoke(pawnType);
    }
}