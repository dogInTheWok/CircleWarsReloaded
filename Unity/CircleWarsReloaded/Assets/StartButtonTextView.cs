using UnityEngine;
using System.Collections;
using Engine;
using UnityEngine.UI;

public class StartButtonTextView : MonoBehaviour {
    public class StateListener : GameState.Listener
    {
        public StateListener( Text parentText )
        {
            this.parentText = parentText;
        }
        public override void OnStateChange(GameState.State state)
        {
            switch( state)
            {
                case GameState.State.NotStarted:
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
        Game.Instance().CurrentState.RegisterOnStateChange(stateListener);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private StateListener stateListener;
}
