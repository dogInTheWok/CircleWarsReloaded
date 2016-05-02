using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

    public static GameObject redToken;

    // Use this for initialization
    void Start () {
        redToken = GameObject.Find("RedToken");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
