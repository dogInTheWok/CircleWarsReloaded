using UnityEngine;
using System.Collections;
using Engine;

public class FieldView : MonoBehaviour {

    [SerializeField] private BoardView boardView;
    private Field field;

    public void Start()
    {
        field = new Field();
    }

    public void OnMouseDown()
    {
        addToken();
    }

    public void addToken()
    {
        if (field.Owner == Player.ID.PLAYER1)
        {
            boardView.addPlayer0Token();
        } else if (field.Owner == Player.ID.PLAYER2)
        {
            boardView.addPlayer1Token();
        }
    }
}
