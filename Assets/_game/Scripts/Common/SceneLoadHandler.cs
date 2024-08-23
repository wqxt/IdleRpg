using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoadHandler", menuName = "ScriptableObject/SceneLoadHandler")]
public class SceneLoadHandler : ScriptableObject
{
    private List<string> _projectSceneList;

    public void Initialization()
    {
        string scenesFolderPath = "Assets/_game/Scenes";
        string[] sceneFiles = Directory.GetFiles(scenesFolderPath, "*.unity");

        _projectSceneList.Clear();

        foreach (var sceneFile in sceneFiles)
        {
            string sceneName = Path.GetFileNameWithoutExtension(sceneFile);
            _projectSceneList.Add(sceneName);
        }

        LoadScene("MainMenu");
    }

    public void LoadScene(string sceneName)
    {
        foreach (var scene in _projectSceneList)
        {
            if (scene == sceneName)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
