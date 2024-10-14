using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapPoint : MonoBehaviour
{
    private void Awake()
    {
        IdleGameState.CurrentState = GameState.MainMenuState;
        GameInputReaderSO inputReaderSO = new GameInputReaderSO();
        inputReaderSO.Initialization();
        SceneManager.LoadScene("MainMenu");
    }
}
