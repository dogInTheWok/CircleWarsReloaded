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
    private GameObject token;
    [SerializeField]
    private GameObject inactiveMarker;

    private List<GameObject> addedTokens;

    // Use this for initialization
    void Start()
    {
        addedTokens = new List<GameObject>();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
    }

    // Update is called once per frame
    void Update()
    {

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
    public void addPlayer1Token(Vector2 pos)
    {
        var addedToken = Instantiate(token);
        addedToken.GetComponent<SpriteRenderer>().color = colorPlayer1;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addPlayer2Token(Vector2 pos)
    {
        var addedToken = Instantiate(token);
        addedToken.GetComponent<SpriteRenderer>().color = colorPlayer2;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addInactiveToken(Vector2 pos)
    {
        var addedToken = Instantiate(inactiveMarker);
        addedToken.GetComponent<SpriteRenderer>().color = colorInactive;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void ClearBoard()
    {
        foreach( GameObject token in addedTokens )
        {
            Destroy(token);
        }
        addedTokens.Clear();
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
}
