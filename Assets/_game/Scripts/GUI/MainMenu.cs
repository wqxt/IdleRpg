using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;


    //������������ unity event
    public void LoadGameplayScene() => _sceneLoadHandler.LoadGameplayScene();

    public void QuitApplication()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
