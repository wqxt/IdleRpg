using UnityEngine;
using UnityEngine.SceneManagement;

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
