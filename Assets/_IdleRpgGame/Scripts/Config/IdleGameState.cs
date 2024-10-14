public static class IdleGameState 
{
    public static GameState CurrentState { get; set; }
}
public enum GameState
{
    EntryState,
    FightState,
    MainMenuState
}