using UnityEngine;
using System.Collections;

public class BoardView : MonoBehaviour {

    [SerializeField]
    private GameObject redToken;
    [SerializeField]
    private GameObject blueToken;
    private GameObject addedToken;

    // Use this for initialization
    void Start () {

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
