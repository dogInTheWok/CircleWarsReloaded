using UnityEngine;
using System.Collections;
using Engine;

public class FieldView : MonoBehaviour {

    [SerializeField] private BoardView boardView;
    private Field field;

    public void Start()
    {
        field = Game.Instance().createField();
    }

    public void OnMouseDown()
    {
        if (!field.addToken())
            return;

        addVisualToken();
    }

    public void addVisualToken()
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
