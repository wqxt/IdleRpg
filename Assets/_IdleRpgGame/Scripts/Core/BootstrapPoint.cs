using System;
using System.Collections;
using System.Runtime.CompilerServices;
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

            GameObject coroutineObject = new GameObject(name: "[COROUTINE]");
            Logger.Log("Create coroutine object", LogLayer.Debug);
            _coroutineController = coroutineObject.AddComponent<CoroutineController>();
            UnityEngine.Object.DontDestroyOnLoad(coroutineObject);
            Logger.Log("Setup coroutine controller", LogLayer.Debug);

        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void SetupGame()
        {
            Logger.InitLoggerSavePath();
            Logger.Log("Message test logger", LogLayer.Debug);

            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Logger.Log($"Targer FPS = {Application.targetFrameRate}", LogLayer.Debug);


            _instance = new BootstrapPoint();
            Logger.Log("Setup bootstrap", LogLayer.Debug);

           _instance.StartGame(); 
            Logger.Log("Start game", LogLayer.Debug);
        }

        public void StartGame()
        {


#if UNITY_EDITOR


            var sceneName = SceneManager.GetActiveScene();


            if (sceneName == null)
            {
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
            }
#endif
       
            _coroutineController.StartCoroutine(LoadScene(SceneName.MAINMENU));
  
        }

        private IEnumerator LoadScene(string targetScene)
        {

            yield return SceneManager.LoadSceneAsync(SceneName.BOOT);   

            yield return SceneManager.LoadSceneAsync(targetScene); 
        }
    }
}