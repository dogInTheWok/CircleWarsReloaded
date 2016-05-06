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

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
