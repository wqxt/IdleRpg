public struct IdleGameState
{
    public static GameState CurrentState { get; set; } = GameState.EntryState;
}
public enum GameState
{
    EntryState,
    FightState,
    MainMenuState
}