﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView: MonoBehaviour {

    class StateListener : CWState<Game.GameState>.Listener
    {
        public StateListener( Text parent)
        {
            text = parent;
        }
        public override void OnStateChange(Game.GameState state )
        {
            text.text = state.ToString();
            if (state == Game.GameState.Terminated)
            {
                text.text = Game.Instance().winner.ToString() + " wins!";
            }
        }
        private Text text;
    }

    [SerializeField] private GameView gameView;
    private StateListener stateListener;
    

    // Use this for initialization
	void Start () {
        var text = GetComponent<Text>();
        stateListener = new StateListener(text);
        gameView.Game.CurrentGameState.ConnectTo(stateListener);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}