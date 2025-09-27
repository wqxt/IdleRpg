using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._IdleRpgGame.Scripts.Core.Utils
{
    public class BootstrapPoint
    {
        private static BootstrapPoint _instance;
        private readonly ICoroutineController _coroutineController;
      
        private BootstrapPoint()
        {
            Debug.Log("[BootstrapPoint] Creating coroutine controller...");
            GameObject coroutineObject = new GameObject(name: "[COROUTINE]");
            _coroutineController = coroutineObject.AddComponent<CoroutineController>();
            UnityEngine.Object.DontDestroyOnLoad(coroutineObject);
            Debug.Log("[BootstrapPoint] Coroutine controller created and marked as DontDestroyOnLoad");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetupGame()
        {
            Debug.Log("[BootstrapPoint] Starting game setup...");
            
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Debug.Log("[BootstrapPoint] Application settings configured - FPS: 60, Sleep: Never");

            _instance = new BootstrapPoint();
            Debug.Log("[BootstrapPoint] BootstrapPoint instance created successfully");
           _instance.StartGame(); 
        }

        public void StartGame()
        {


#if UNITY_EDITOR


            var sceneName = SceneManager.GetActiveScene();
            Debug.Log($"[BootstrapPoint] Current active scene: {sceneName.name ?? "null"}");

            if (sceneName == null)
            {
                Debug.LogWarning("[BootstrapPoint] SceneName is Null. Return.");
                return;
            }

            if (sceneName.name.Equals(SceneName.GAMEPLAY, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("[BootstrapPoint] Current scene is GAMEPLAY, loading GAMEPLAY scene");
                _coroutineController.StartCoroutine(LoadScene(SceneName.GAMEPLAY));
                return;
            }

            if (!sceneName.name.Equals(SceneName.BOOT, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("[BootstrapPoint] Current scene is not BOOT, loading MAINMENU scene");
                _coroutineController.StartCoroutine(LoadScene(SceneName.MAINMENU));
            }
#endif
            Debug.Log("[BootstrapPoint] Loading MAINMENU scene (default behavior)");
       
            _coroutineController.StartCoroutine(LoadScene(SceneName.MAINMENU));
  
        }

        private IEnumerator LoadScene(string targetScene)
        {

            yield return SceneManager.LoadSceneAsync(SceneName.BOOT);   // ��������� ������� ����� ������ �����
            Debug.Log("Load BOOT successfully");

            yield return SceneManager.LoadSceneAsync(targetScene); // ��������� ������ �����
            Debug.Log($"Load {targetScene} successfully");
        }
    }
}