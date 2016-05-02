using UnityEngine;
using System.Collections;
using Engine;

public class FieldView : MonoBehaviour {

    private Field field;

    public int fieldID = -1;
    

    public void OnMouseDown()
    {
        Instantiate(Board.redToken);
    }

}
