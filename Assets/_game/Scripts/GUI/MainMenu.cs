using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    //Unity event
    public void LoadGameplayScene() => _sceneLoadHandler.LoadScene("GamePlay");
    public void QuitApplication() => Application.Quit();
}
