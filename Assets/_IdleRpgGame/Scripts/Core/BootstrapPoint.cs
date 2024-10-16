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
    public static void SetupGame()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _instance = new BootstrapPoint();
        _instance.StartGame();
    }

    private void StartGame()
    {

#if UNITY_EDITOR

        var sceneName = SceneManager.GetActiveScene();

        if (sceneName == null)
        {
            Debug.LogWarning("SceneName is Null.");
            return;
        }

        if (sceneName.name.Equals(SceneName.GAMEPLAY, StringComparison.OrdinalIgnoreCase))
        {
            _coroutineController.StartCoroutine(StartGameplay());
            return;
        }

        if (!sceneName.name.Equals(SceneName.BOOT, StringComparison.OrdinalIgnoreCase))
        {
            StartMainMenu();
            return;
        }
#endif
        _coroutineController.StartCoroutine(StartMainMenu());
    }

    private IEnumerator StartGameplay()
    {

        yield return SceneManager.LoadSceneAsync(SceneName.BOOT);
        Debug.Log("Load BOOT successfully");

        yield return SceneManager.LoadSceneAsync(SceneName.GAMEPLAY);
        Debug.Log("Load MAINMENU successfully");
    }

    private IEnumerator StartMainMenu()
    {
        yield return SceneManager.LoadSceneAsync(SceneName.BOOT);
        Debug.Log("Load successfully");

        yield return SceneManager.LoadSceneAsync(SceneName.MAINMENU);
        Debug.Log("Load successfully");
    }
}