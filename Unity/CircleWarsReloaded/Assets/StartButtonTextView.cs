using UnityEngine;
using System.Collections;
using Engine;
using UnityEngine.UI;

public class StartButtonTextView : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
    }

    // Implement slot on GameState
    public void OnStateChange(Game.GameState state)
    {
        switch (state)
        {
            case Game.GameState.NotStarted:
                text.text = "Start";
                break;
            default:
                text.text = "Reset";
                break;
        }
    }
}
