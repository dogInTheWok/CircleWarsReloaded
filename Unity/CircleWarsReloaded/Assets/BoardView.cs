using UnityEngine;
using System.Collections;

public class BoardView : MonoBehaviour {

    private GameObject redToken;
    private GameObject blueToken;
    private GameObject addedToken;

    // Use this for initialization
    void Start () {
        redToken = GameObject.Find("RedToken");
        redToken = GameObject.Find("BlueToken");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPlayer0Token()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        addedToken = Instantiate(blueToken);
        addedToken.transform.position = new Vector2(p.x, p.y);
    }

    public void addPlayer1Token()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        addedToken = Instantiate(redToken);
        addedToken.transform.position = new Vector2(p.x, p.y);
    }
}
