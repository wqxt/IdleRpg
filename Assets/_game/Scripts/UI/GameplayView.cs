using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayView : MonoBehaviour
{
    //unity button 
    public void MainMenu()
    {
        IdleGameState.CurrentState = GameState.MainMenuState;
        SceneManager.LoadScene("MainMenu");
    }

    //unity button 
    public void StartFight() => IdleGameState.CurrentState = GameState.FightState;

    //unity button 
    public void Escape() => IdleGameState.CurrentState = GameState.EntryState;
}