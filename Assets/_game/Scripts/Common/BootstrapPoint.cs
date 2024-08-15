using UnityEngine;

public class BootstrapPoint : MonoBehaviour
{
    [SerializeField] private GameInputReaderSO _gameInputReaderSO;
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    private void Awake()
    {
        _gameInputReaderSO.Initialization();
        _sceneLoadHandler.Initialization();
    }
}
