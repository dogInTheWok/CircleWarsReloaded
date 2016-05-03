using UnityEngine;
using System.Collections;
using Engine;

public class GameView : MonoBehaviour {
    private Game game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClickStartButton()
    {
        Debug.Log("Game has started.");
    }
}
