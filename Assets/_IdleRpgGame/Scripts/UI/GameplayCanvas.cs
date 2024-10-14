using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvas : MonoBehaviour
{

    //Unity event
    public void MainMenu()
    {
        IdleGameState.CurrentState = GameState.MainMenuState;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartFight()
    {
        IdleGameState.CurrentState = GameState.FightState;
    }

    public void Escape() => IdleGameState.CurrentState = GameState.EntryState;
}