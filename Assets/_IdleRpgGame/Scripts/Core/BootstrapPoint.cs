using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapPoint
{
    private static BootstrapPoint _instance;
    private readonly ICoroutineController _coroutineController;

    private BootstrapPoint()
    {
        GameObject coroutineObject = new GameObject(name: "[COROUTINE]");
        _coroutineController = coroutineObject.AddComponent<CoroutineController>();
        UnityEngine.Object.DontDestroyOnLoad(coroutineObject);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void SetupGame()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _instance = new BootstrapPoint();
        _instance.StartGame();
    }

    public void StartGame()
    {

#if UNITY_EDITOR

        var sceneName = SceneManager.GetActiveScene();

        if (sceneName == null)
        {
            Debug.LogWarning("SceneName is Null. Return.");
            return;
        }

        if (sceneName.name.Equals(SceneName.GAMEPLAY, StringComparison.OrdinalIgnoreCase))
        {
            _coroutineController.StartCoroutine(LoadScene(SceneName.GAMEPLAY));
            return;
        }

        if (!sceneName.name.Equals(SceneName.BOOT, StringComparison.OrdinalIgnoreCase))
        {
            _coroutineController.StartCoroutine(LoadScene(SceneName.MAINMENU));
            return;
        }
#endif
        _coroutineController.StartCoroutine(LoadScene(SceneName.MAINMENU));
    }

    private IEnumerator LoadScene(string targetScene)
    {

        yield return SceneManager.LoadSceneAsync(SceneName.BOOT);   //unloading resources
        Debug.Log("Load BOOT successfully");

        yield return SceneManager.LoadSceneAsync(targetScene);  
        Debug.Log($"Load {targetScene} successfully");
    }
}