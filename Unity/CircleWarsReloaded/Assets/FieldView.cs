using UnityEngine;
using System.Collections;
using Engine;
using System;

public class FieldView : MonoBehaviour
{
    [SerializeField]
    private BoardView boardView;
    [SerializeField]
    private FieldView[] neighbours;
    [SerializeField]
    private Vector2 identPoint;

    private Vector2 tokenPoint;

    private Vector2 NextTokenPositionIncrement = new Vector2(15, 5);

    public Field Field { get; private set; }

    public void Start()
    {
        Field = Game.Instance().CreateField();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
        tokenPoint = identPoint;
    }

    public void OnMouseDown()
    {
        var activePlayer = Game.Instance().ActivePlayer();
        if (!activePlayer.Client.AddToken(Field))
            return;

        AddVisualToken();
    }

    // Implement Slot for GameState
    public void OnStateChange(Game.GameState state)
    {
        switch (state)
        {
            case Game.GameState.Evaluating:
                addNeighbours();
                AddVisualEvaluationResult();
                break;
            case Game.GameState.NotStarted:
                resetVisuals();
                break;
            default:
                break;
        }
    }

    public void AddVisualToken()
    {
        if (Field.Owner == Player.Id.PLAYER1)
        {
            boardView.AddPlayer1Token(tokenPoint);
        }
        else if (Field.Owner == Player.Id.PLAYER2)
        {
            boardView.AddPlayer2Token(tokenPoint);
        }
        tokenPoint += NextTokenPositionIncrement;
    }
    public void AddVisualThreeTokens()
    {
        AddVisualToken();
        AddVisualToken();
        AddVisualToken();
    }
    public void AddVisualInactiveMarker()
    {
        boardView.AddInactiveToken(identPoint);
    }
    public void AddVisualEvaluationResult()
    {
        if (Field.HasBatillion)
        {
            AddVisualThreeTokens();
        }
        else if (Field.HasMarine)
        {
            AddVisualToken();
        }

        if (!Field.IsActive)
            AddVisualInactiveMarker();

    }

    private void addNeighbours()
    {
        foreach (FieldView view in neighbours)
        {
            Field.addNeighbour(view.Field);
        }
    }
    private void resetVisuals()
    {
        tokenPoint = identPoint;
    }
}
