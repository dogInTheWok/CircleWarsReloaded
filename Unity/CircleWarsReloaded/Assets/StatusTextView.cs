using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView : MonoBehaviour {

    [SerializeField] private GameView gameView;

    // Use this for initialization
	void Start () {
        var game = gameView.Game;
        var text = GetComponent<Text>();
        text.text = "Started";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
