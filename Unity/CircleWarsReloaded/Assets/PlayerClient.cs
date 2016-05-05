using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Engine;

public class PlayerClient : NetworkBehaviour
{
    [SyncVar(hook = "OnGameStateChangeIn")]
    Game.GameState gameState;
    [SyncVar(hook = "OnSecretPhaseChangeIn")]
    Game.SecretPhaseState secretPhase;
    [SyncVar(hook = "OnActivePlayerChangeIn")]
    Player.Id activePlayer;

    private Game game;
    // The Player representation in Engine.
    private Player player;
    private bool hasBeenSynced;

    // Use this for initialization
    void Start()
    {
        game = Game.Instance();
        player = game.CreatePlayer(this);
        hasBeenSynced = false;
        // Connect to states
        Game.Instance().CurrentGameState.ConnectTo(OnGameStateChangeOut);
        Game.Instance().CurrentSecretPhaseState.ConnectTo(OnSecretPhaseChangeOut);
        Game.Instance().ActivePlayerId().ConnectTo(OnActivePlayerChangeOut);
    }
    
    public void OnGameStateChangeIn(Game.GameState state)
    {
        CWLogging.Instance().LogDebug("Syned State:" + gameState + state);
        hasBeenSynced = true;
        gameState = state;
        game.CurrentGameState.Value = gameState;
    }
    public void OnSecretPhaseChangeIn(Game.SecretPhaseState state)
    {
        hasBeenSynced = true;
        game.CurrentSecretPhaseState.Value = secretPhase;
    }
    public void OnActivePlayerChangeIn(Player.Id state)
    {
        hasBeenSynced = true;
        game.ActivePlayerId().Value = activePlayer;
    }
    
    public void OnGameStateChangeOut(Game.GameState state)
    {
        CmdSyncGameState(state);
    }
    public void OnSecretPhaseChangeOut(Game.SecretPhaseState state)
    {
        CmdSyncSecretState(state);
    }
    public void OnActivePlayerChangeOut(Player.Id state)
    {
        CmdSyncActivePlayer(state);
    }

    [Command]
    void CmdSyncGameState(Game.GameState state)
    {
        CWLogging.Instance().LogDebug("Start snyced: GameState");
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        gameState = state;
    }

    [Command]
    void CmdSyncSecretState(Game.SecretPhaseState state)
    {

        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        secretPhase = state;
    }

    [Command]
    void CmdSyncActivePlayer(Player.Id id)
    {
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        activePlayer = id;

    }
}
