using UnityEngine;
using System.Collections;
using Engine;

public class FieldView : MonoBehaviour {

    private Field field;
    private BoardView bView; 

    public void Start()
    {
        bView = (BoardView)GetComponent("BoardView");
    }

    public void OnMouseDown()
    {
        addToken();
    }

    public void addToken()
    {
        if (field.Owner == Player.ID.PLAYER1)
        {
            bView.addPlayer0Token();
        } else if (field.Owner == Player.ID.PLAYER2)
        {
            bView.addPlayer1Token();
        }
    }
}
