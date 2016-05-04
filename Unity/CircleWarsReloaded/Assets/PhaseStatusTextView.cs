using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Engine;
public class PhaseStatusTextView : MonoBehaviour
{
    private Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        var game = Game.Instance();
        game.CurrentGameState.ConnectTo(OnGameStateChanged);
        game.CurrentSecretPhaseState.ConnectTo(OnPhaseChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGameStateChanged(Game.GameState state)
    {
        switch( state )
        {
            case Game.GameState.NotStarted:
                text.text = "Not started yet";
                break;
            case Game.GameState.RunningDistribution:
                text.text = "Distribute Coins.";
                break;
            case Game.GameState.RunningSecret:
                OnPhaseChanged(Game.Instance().CurrentSecretPhaseState.Value);
                break;
            default:
                text.text = "No Status found";
                break;
        }
    }

    void OnPhaseChanged(Game.SecretPhaseState state)
    {
        switch( state )
        {
            case Game.SecretPhaseState.Batillion:
                text.text = "Batillion Draw";
                break;
            case Game.SecretPhaseState.Marine:
                text.text = "Marine Draw";
                break;
            case Game.SecretPhaseState.Napalm:
                text.text = "Napalm Draw";
                break;
            default:
                text.text = "No Status found.";
                break;
        }
    }
}
