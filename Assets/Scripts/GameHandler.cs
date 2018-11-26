
// GameState sahip On ana ekran da, Start oyun oynarken, Over da bitiş ekranı
public class GameHandler {

    public GameState game;

    public enum GameState
    {
        Start,
        On,
        Over
    };

    public GameHandler(GameState state)
    {
        game = state;
    }

    public void GameOver()
    {
        game = GameState.Over;
        Platform.instance.runner.GetComponent<Runner>().enabled = false;
    }
    public void StartGame()
    {
        game = GameState.Start;
    }

}
