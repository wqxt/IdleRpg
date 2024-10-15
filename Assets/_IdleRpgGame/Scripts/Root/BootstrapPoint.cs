using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapPoint
{
    private static BootstrapPoint _instance;
    public Coroutines _coroutines;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Setup()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _instance = new BootstrapPoint();
        _instance.StartGame();
    }

    private BootstrapPoint()
    {
        _coroutines = new GameObject(name:"[COROUTINES]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(_coroutines);
    }

    private void StartGame()
    {
        _coroutines.StartCoroutine(LoadAndStarGameplay());
    }

    private IEnumerator LoadAndStarGameplay()
    {
        SceneManager.LoadScene("MainMenu");
        yield return null;  
    }
}
