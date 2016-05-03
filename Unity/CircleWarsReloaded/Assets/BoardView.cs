using UnityEngine;
using System.Collections;

public class BoardView : MonoBehaviour {

    [SerializeField]
    private GameObject redToken;
    [SerializeField]
    private GameObject blueToken;
    [SerializeField]
    private GameObject blackToken;
    private GameObject addedToken;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPlayer0Token( Vector2 pos )
    {
        addedToken = Instantiate(blueToken);
        addedToken.transform.position = pos;
    }

    public void addPlayer1Token( Vector2 pos )
    {
        addedToken = Instantiate(redToken);
        addedToken.transform.position = pos;
    }

    public void addInactiveToken( Vector2 pos )
    {
        addedToken = Instantiate(blackToken);
        addedToken.transform.position = pos;
    }
}
