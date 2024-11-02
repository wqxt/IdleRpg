using UnityEngine;
using UnityEngine.SceneManagement;
using Assets._IdleRpgGame.Scripts.Core.Utils;

public class MainMenuView : MonoBehaviour
{
    //unity button 
    public void LoadGameplayScene()
    {
        IdleGameState.CurrentState = GameState.EntryState;
        SceneManager.LoadScene(SceneName.GAMEPLAY);
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log($"Quit application");
    }
}