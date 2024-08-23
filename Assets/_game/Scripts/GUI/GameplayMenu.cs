using UnityEngine;

public class GameplayMenu : MonoBehaviour
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    //Unity event
    public void ExitFight() => _sceneLoadHandler.LoadScene("MainMenu");
}