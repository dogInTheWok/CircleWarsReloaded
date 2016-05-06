using UnityEngine;
using System.Collections;
using Engine;

public class GameView : MonoBehaviour
{
    public Game Game { get; private set; }

    void Awake()
    {
        Game = Game.Instance();
    }
    public void OnClickStartButton()
    {
        if( Game.CurrentGameState.Value == Game.GameState.NotStarted )
        {
            Game.StartGame();
        }
        else
        {
            Game.Reset();
        }
    }
}
