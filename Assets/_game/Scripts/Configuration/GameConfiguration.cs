using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObject/GameConfiguration")]
public class GameConfiguration : ScriptableObject
{
    public GameState CurrentState { get; set; }
}
public enum GameState
{
    EntryState,
    FightState,
    MainMenuState
}