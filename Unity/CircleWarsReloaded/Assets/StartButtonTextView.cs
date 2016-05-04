using UnityEngine;
using System.Collections;
using Engine;
using UnityEngine.UI;

public class StartButtonTextView : MonoBehaviour
{
    private Text text;        

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        Game.Instance().CurrentGameState.ConnectTo(OnStateChange);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    // Implement slot on GameState
    public void OnStateChange(Game.GameState state)
    {
        switch (state)
        {
            case Game.GameState.NotStarted:
                text.text = "Start";
                break;
            case Game.GameState.Terminated:
                text.text = "Start";
                break;
            default:
                text.text = "Reset";
                break;
        }
    }
}
