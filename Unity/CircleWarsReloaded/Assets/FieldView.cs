using UnityEngine;
using System.Collections;
using Engine;
using System;

public class FieldView : MonoBehaviour {
    public class StateListener : GameState.Listener
    {
        public StateListener( FieldView parent)
        {
            this.parent = parent;
        }
        public override void OnStateChange(GameState.State state)
        {
            if( GameState.State.Eval == state )
            {
                parent.AddNeighbours();
                parent.AddThreeVisualTokens();
            }
        }
        private FieldView parent;
    } 

    [SerializeField] private BoardView boardView;
    [SerializeField] private FieldView[] neighbours;
    private StateListener stateListener;

    public Field Field { get; private set; }

    public void Start()
    {
        Field = Game.Instance().createField();
        stateListener = new StateListener( this );
        Game.Instance().CurrentState.RegisterOnStateChange(stateListener);
    }

    public void OnMouseDown()
    {
        if (!Field.requestToken())
            return;

        addVisualToken();
    }

    public void addVisualToken()
    {

        if (Field.Owner == Player.ID.PLAYER1)
        {
            boardView.addPlayer0Token();
        } else if (Field.Owner == Player.ID.PLAYER2)
        {
            boardView.addPlayer1Token();
        }

    }

    public void AddThreeVisualTokens()
    {
        Debug.Log("Test Add three tokens");
    }
    public void AddNeighbours()
    {
        foreach (FieldView view in neighbours)
        {
            Field.addNeighbour(view.Field);
        }
    }
}
