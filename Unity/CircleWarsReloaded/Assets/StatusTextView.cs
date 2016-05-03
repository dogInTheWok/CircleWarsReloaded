using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView : MonoBehaviour {

    [SerializeField] private GameView gameView;
    private Text text;
    // Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        text.text = gameView.Game.CurrentState.Value.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickStartButton()
    {
        text.text = gameView.Game.CurrentState.Value.ToString();
    }
}