using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView : MonoBehaviour
{
    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
    }

    // Implement slot on GameStateChange
    public void OnStateChange(Game.GameState state)
    {
        if (state == Game.GameState.Terminated)
        {
            text.text = Game.Instance().Winner.ToString() + " wins!";
        }
        else
        {
            text.text = state.ToString();
        }
    }
}