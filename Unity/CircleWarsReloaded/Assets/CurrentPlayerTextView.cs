using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class CurrentPlayerTextView : MonoBehaviour
{
    [SerializeField]
    private BoardView boardView;

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        Game.Instance().ActivePlayer().ConnectTo(OnPlayerChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPlayerChanged(Player.ID activePlayer )
    {
        CWLogging.Instance().LogDebug("Player changed");
        CWLogging.Instance().LogDebug(activePlayer.ToString());
        switch( activePlayer )
        {
            case Player.ID.PLAYER1:
                text.text = "Player 1";
                text.color = boardView.ColorPlayer1();
                break;
            case Player.ID.PLAYER2:
                text.text = "Player 2";
                text.color = boardView.ColorPlayer2();
                break;
            default:
                text.text = "Invalid";
                text.color = boardView.ColorInactive();
                break;
        }
    }
}
