using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapPoint : MonoBehaviour
{
    [SerializeField] private GameInputReaderSO _gameInputReaderSO;
    [SerializeField] private GameConfiguration GameConfiguration;

    private void Awake()
    {
        GameConfiguration.CurrentState = GameState.MainMenuState;
        _gameInputReaderSO.Initialization();
        SceneManager.LoadScene("MainMenu");
    }
}