﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;

public class StatusTextView: MonoBehaviour {

    class StateListener : GameState.Listener
    {
        public StateListener( ref Text parent)
        {

        }
        public override void OnStateChange(GameState.State state )
        {
            text.text = state.ToString();
        }
        private Text text;
    }

    [SerializeField] private GameView gameView;
    private StateListener stateListener;
    

    // Use this for initialization
	void Start () {
        var text = GetComponent<Text>();
        stateListener = new StateListener(text);
        gameView.Game.CurrentState.RegisterOnStateChange(stateListener);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}