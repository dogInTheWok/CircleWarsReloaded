using UnityEngine;
using System.Collections;
using Engine;
using UnityEngine.UI;

public class StartButtonTextView : MonoBehaviour {
    public class StateListener : CWState<Game.GameState>.Listener
    {
        public StateListener( Text parentText )
        {
            this.parentText = parentText;
        }
        public override void OnStateChange(Game.GameState state)
        {
            switch( state)
            {
                case Game.GameState.NotStarted:
                    parentText.text = "Start";
                    break;
                case Game.GameState.Terminated:
                    parentText.text = "Start";
                    break;
                default:
                    parentText.text = "Restart";
                    break;
            }
        }

        private Text parentText;
    }
    

	// Use this for initialization
	void Start () {
        stateListener = new StateListener(GetComponent<Text>());
        Game.Instance().CurrentGameState.ConnectTo(stateListener);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private StateListener stateListener;
}
