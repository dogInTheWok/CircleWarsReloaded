using UnityEngine;
using System.Collections;


public class Field : MonoBehaviour {

    public int fieldID = -1;
    

    public void OnMouseDown()
    {
        Instantiate(Board.redToken);
    }

}
