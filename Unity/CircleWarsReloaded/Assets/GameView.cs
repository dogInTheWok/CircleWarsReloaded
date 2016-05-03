using UnityEngine;
using System.Collections;
using Engine;

public class GameView : MonoBehaviour {
    public Game Game { get; private set; }

	// Use this for initialization
	void Start () {
        Game = Game.Instance();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClickStartButton()
    {
        Debug.Log("Game has started.");
    }
}
