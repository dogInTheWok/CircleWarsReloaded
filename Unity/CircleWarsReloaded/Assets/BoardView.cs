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
    private GameObject playerToken;
    [SerializeField]
    private GameObject inactiveToken;

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
    public void addPlayer0Token(Vector2 pos)
    {
        var addedToken = Instantiate(playerToken);
        addedToken.GetComponent<SpriteRenderer>().color = colorPlayer1;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addPlayer1Token(Vector2 pos)
    {
        var addedToken = Instantiate(playerToken);
        addedToken.GetComponent<SpriteRenderer>().color = colorPlayer2;
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addInactiveToken(Vector2 pos)
    {
        var addedToken = Instantiate(inactiveToken);
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
}
