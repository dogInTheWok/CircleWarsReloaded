using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Engine;
using System;

public class BoardView : MonoBehaviour {
    public class StateListener : GameState.Listener
    {
        public StateListener( BoardView parent )
        {
            this.parent = parent;
        }
        public override void OnStateChange(GameState.State state)
        {
            if( GameState.State.NotStarted == state )
            {
                parent.ClearBoard();
            }
        }
        private BoardView parent;
    }

    [SerializeField]
    private GameObject redToken;
    [SerializeField]
    private GameObject blueToken;
    [SerializeField]
    private GameObject blackToken;

    private List<GameObject> addedTokens;

    // Use this for initialization
    void Start () {
        stateListener = new StateListener(this);
        Game.Instance().CurrentState.RegisterOnStateChange(stateListener);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPlayer0Token( Vector2 pos )
    {
        var addedToken = Instantiate(blueToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addPlayer1Token( Vector2 pos )
    {
        var addedToken = Instantiate(redToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void addInactiveToken( Vector2 pos )
    {
        var addedToken = Instantiate(blackToken);
        addedToken.transform.position = pos;
        addedTokens.Add(addedToken);
    }

    public void ClearBoard()
    {
        addedTokens.Clear();
    }

    private StateListener stateListener;
}
