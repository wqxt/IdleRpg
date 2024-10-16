using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    //Unity event
    public void LoadGameplayScene()
    {
        IdleGameState.CurrentState = GameState.EntryState;
        SceneManager.LoadScene(SceneName.GAMEPLAY);
    }
    public void QuitApplication() => Application.Quit();
}
