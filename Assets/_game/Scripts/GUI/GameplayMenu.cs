using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenu : MonoBehaviour
{

    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    //Используются unity event
    public void ExitFight() => _sceneLoadHandler.LoadMainMenu();

}
