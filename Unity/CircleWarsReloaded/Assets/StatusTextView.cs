using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView : MonoBehaviour
{
    [SerializeField]
    private GameView gameView;

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        gameView.Game.CurrentGameState.ConnectTo(OnStateChange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Implement slot on GameStateChange
    public void OnStateChange(Game.GameState state)
    {
        text.text = state.ToString();
        if (state == Game.GameState.Terminated)
        {
            text.text = Game.Instance().winner.ToString() + " wins!";
        }
    }
}