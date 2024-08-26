using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameConfiguration _gameConfiguration;

    //Unity event
    public void LoadGameplayScene()
    {
        _gameConfiguration.CurrentState = GameState.EntryState;
        SceneManager.LoadScene("Gameplay");
    }
    public void QuitApplication() => Application.Quit();
}
