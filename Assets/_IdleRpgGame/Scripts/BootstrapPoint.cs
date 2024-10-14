using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapPoint : MonoBehaviour
{
    [SerializeField] private GameInputReaderSO _gameInputReaderSO;

    private void Awake()
    {
        IdleGameState.CurrentState = GameState.MainMenuState;
        _gameInputReaderSO.Initialization();
        SceneManager.LoadScene("MainMenu");
    }
}
