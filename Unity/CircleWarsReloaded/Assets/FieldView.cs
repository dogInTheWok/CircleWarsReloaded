using UnityEngine;
using System.Collections;
using Engine;
using System;

public class FieldView : MonoBehaviour {


    [SerializeField] private BoardView boardView;
    [SerializeField] private FieldView[] neighbours;
    [SerializeField] private Vector2 identPoint;
    private Vector2 tokenPoint;
    private Vector2 fieldPosition;

    private static Vector2 MOVE_NEXT_TOKEN = new Vector2(15, 5);

    public Field Field { get; private set; }

    public void Start()
    {
        Field = Game.Instance().createField();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
        tokenPoint = identPoint;
    }

    public void OnMouseDown()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!Field.requestToken())
            return;

        addVisualToken();
    }

    // Implement Slot for GameState
    public void OnStateChange(Game.GameState state)
    {
        switch (state)
        {
            case Game.GameState.Evaluating:
                AddNeighbours();
                AddEvalVisuals();
                break;
            case Game.GameState.NotStarted:
                ResetVisuals();
                break;
        }
    }

    public void addVisualToken()
    {
        if (Field.Owner == Player.ID.PLAYER1)
        {
            boardView.addPlayer1Token(tokenPoint);
        } else if (Field.Owner == Player.ID.PLAYER2)
        {
            boardView.addPlayer2Token(tokenPoint);
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
        boardView.addInactiveToken(identPoint);
    }

    public void AddNeighbours()
    {
        foreach (FieldView view in neighbours)
        {
            Field.addNeighbour(view.Field);
        }
    }

    public void AddEvalVisuals()
    {
        if (Field.HasBatillion)
        {
            AddThreeVisualTokens();
        }
        else if (Field.HasMarine)
        {
            addVisualToken();
        }

        if (!Field.IsActive)
            AddBlackToken();

    }

    public void ResetVisuals()
    {
        tokenPoint = identPoint;
    }
}
