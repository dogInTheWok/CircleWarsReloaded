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

                if( parent.Field.HasBatillion )
                {
                    parent.AddThreeVisualTokens();
                }
                else if( parent.Field.HasMarine )
                {
                    parent.addVisualToken();
                }

                parent.AddBlackToken();
            }
        }
        private FieldView parent;
    } 

    [SerializeField] private BoardView boardView;
    [SerializeField] private FieldView[] neighbours;
    [SerializeField] private Vector2 identPoint;
    private Vector2 tokenPoint;
    private StateListener stateListener;
    private Vector2 fieldPosition;

    private static Vector2 MOVE_NEXT_TOKEN = new Vector2(15, 5);

    public Field Field { get; private set; }

    public void Start()
    {
        Field = Game.Instance().createField();
        stateListener = new StateListener( this );
        Game.Instance().CurrentGameState.RegisterOnStateChange(stateListener);
        tokenPoint = identPoint;
    }

    public void OnMouseDown()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(p);
        if (!Field.requestToken())
            return;

        addVisualToken();
    }

    public void addVisualToken()
    {
        if (Field.Owner == Player.ID.PLAYER1)
        {
            boardView.addPlayer0Token(tokenPoint);
        } else if (Field.Owner == Player.ID.PLAYER2)
        {
            boardView.addPlayer1Token(tokenPoint);
        }
        tokenPoint += MOVE_NEXT_TOKEN; 
    }

    public void AddThreeVisualTokens()
    {
        addVisualToken();
        addVisualToken();
        addVisualToken();
    }

    public void AddBlackToken()
    {
        if (!Field.IsActive)
            boardView.addInactiveToken(identPoint);
    }

    public void AddNeighbours()
    {
        foreach (FieldView view in neighbours)
        {
            Field.addNeighbour(view.Field);
        }
    }
}
