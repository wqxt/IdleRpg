using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    public void LoadGameplayScene() => _sceneLoadHandler.LoadGameplayScene();

    public void QuitApplication() => Application.Quit();

}
