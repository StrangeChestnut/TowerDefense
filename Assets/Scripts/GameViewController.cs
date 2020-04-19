using UnityEngine;

public class GameViewController
{
    public void OnOpen(GameView view)
    {
        view.game.UpdateScoreEvent += view.UpdateScore;
        view.game.UpdateWaveEvent += view.UpdateWave;
        view.game.UpdateTimerEvent += view.UpdateTimer;
        view.game.ChangeCastleHealthEvent += view.UpdateCastleHealth;
    }

    public void OnClose(GameView view)
    {
        view.game.UpdateScoreEvent -= view.UpdateScore;
        view.game.UpdateWaveEvent -= view.UpdateWave;
        view.game.UpdateTimerEvent -= view.UpdateTimer;
        view.game.ChangeCastleHealthEvent -= view.UpdateCastleHealth;
    }
}