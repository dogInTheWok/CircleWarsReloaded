using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;
using System;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private GameObject redToken;
    [SerializeField]
    private GameObject blueToken;
    [SerializeField]
    private GameObject blackToken;

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
            ClearBoard();
        }
    }
    public void addPlayer0Token(Vector2 pos)
    {
        var addedToken = Instantiate(blueToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addPlayer1Token(Vector2 pos)
    {
        var addedToken = Instantiate(redToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addInactiveToken(Vector2 pos)
    {
        var addedToken = Instantiate(blackToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void ClearBoard()
    {
        addedTokens.Clear();
    }
}
