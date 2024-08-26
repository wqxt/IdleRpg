using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private GameConfiguration _gameConfiguration;
    [SerializeField] private PawnConfiguration _characterConfiguration;
    [SerializeField] private GameObject _healButton;

    private void Awake() => _gameConfiguration.CurrentState = GameState.EntryState;
    private void LateUpdate()
    {
        if (_gameConfiguration.CurrentState == GameState.EntryState) 
        {
            _healButton.SetActive(true);
        }

    }
    //Unity event
    public void MainMenu()
    {
        _gameConfiguration.CurrentState = GameState.MainMenuState;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartFight()
    {
        _healButton.SetActive(false);
        _gameConfiguration.CurrentState = GameState.FightState;
    }

    public void Escape() => _gameConfiguration.CurrentState = GameState.EntryState;
    public void Heal() => _characterConfiguration.CurrentHealthValue = _characterConfiguration.StartHealthValue;
}