using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;
using System;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private Color colorPlayer1;
    [SerializeField]
    private Color colorPlayer2;
    [SerializeField]
    private Color colorInactive;
    [SerializeField]
    private GameObject inactiveMarker;

    private List<GameObject> addedTokens;

    void Awake()
    {
        addedTokens = new List<GameObject>();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
    }

    // Connection to GameState
    public void OnStateChange(Game.GameState state)
    {
        if (Game.GameState.NotStarted == state)
        {
            CWLogging.Instance().LogDebug("Clearing Board.");
            ClearBoard();
        }
    }

    public Color ColorPlayer1()
    {
        return colorPlayer1;
    }
    public Color ColorPlayer2()
    {
        return colorPlayer2;
    }
    public Color ColorInactive()
    {
        return colorInactive;
    }

    public void AddPlayer1Token(Vector2 pos)
    {
        Game.Instance().ActivePlayer().Client.SpawnToken(pos, colorPlayer1);
    }
    public void AddPlayer2Token(Vector2 pos)
    {
        Game.Instance().ActivePlayer().Client.SpawnToken(pos, colorPlayer2);
    }
    public void AddInactiveToken(Vector2 pos)
    {
        //TODO: Move to some NetworkBehavior
        var addedToken = Instantiate(inactiveMarker);
        addedToken.GetComponent<SpriteRenderer>().color = colorInactive;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }
    public void ClearBoard()
    {
        Game.Instance().ClearPlayerTokens();
    }

}
