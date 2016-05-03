using UnityEngine;
using System.Collections;
using Engine;

public class FieldView : MonoBehaviour {

    [SerializeField] private BoardView boardView;
    [SerializeField] private FieldView[] neighbours;

    public Field Field { get; private set; }

    public void Start()
    {
        Field = Game.Instance().createField();

        foreach( FieldView view in neighbours)
        {
            Field.addNeighbour(view.Field);
        }
    }

    public void OnMouseDown()
    {
        if (!Field.addToken())
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
}
