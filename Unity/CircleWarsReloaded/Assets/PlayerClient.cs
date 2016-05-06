using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Engine;
using System.Collections.Generic;

public class PlayerClient : NetworkBehaviour
{
    [SyncVar(hook = "OnGameStateChangeIn")]
    Game.GameState gameState;
    [SyncVar(hook = "OnSecretPhaseChangeIn")]
    Game.SecretPhaseState secretPhase;
    [SyncVar(hook = "OnActivePlayerChangeIn")]
    Player.Id activePlayer;
    [SyncVar(hook = "OnDistribTurnChangeIn")]
    private int distribTurn;

    [SerializeField]
    private GameObject token;

    private Game game;
    public List<GameObject> addedTokens;

    // The Player representation in Engine.
    private Player player;
    private bool hasBeenSynced = false;

    // Use this for initialization
    void Start()
    {
        game = Game.Instance();
        player = game.CreatePlayer(this);
        // Connect to states
        
            Game.Instance().CurrentGameState.ConnectTo(OnGameStateChangeOut);
            Game.Instance().CurrentSecretPhaseState.ConnectTo(OnSecretPhaseChangeOut);
            Game.Instance().ActivePlayerId().ConnectTo(OnActivePlayerChangeOut);
     
        CWLogging.Instance().LogDebug("PlayerClient started: " + player.PlayerId.ToString());
    }
    
    public void OnGameStateChangeIn(Game.GameState state)
    {
        CWLogging.Instance().LogDebug("Synced GameState:" + state);
        hasBeenSynced = true;
        gameState = state;
        game.CurrentGameState.Value = gameState;
    }
    public void OnSecretPhaseChangeIn(Game.SecretPhaseState state)
    {
        CWLogging.Instance().LogDebug("Synced SecretPhaseState:" + state);
        hasBeenSynced = true;
        secretPhase = state;
        game.CurrentSecretPhaseState.Value = secretPhase;
    }
    public void OnActivePlayerChangeIn(Player.Id state)
    {
        CWLogging.Instance().LogDebug("Synced Player:" + state);
        hasBeenSynced = true;
        activePlayer = state;
        game.ActivePlayerId().Value = state;
    }
    public void OnDistribTurnChangeIn(int turn)
    {
        CWLogging.Instance().LogDebug("PlayerClient::OnDistribTurnChange: Synced Turn: " + turn);
        hasBeenSynced = true;
        distribTurn = turn;
        game.DistribTurn = distribTurn;
    }

    public void OnGameStateChangeOut(Game.GameState state)
    {
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        CmdSyncGameState(state);
    }
    public void OnSecretPhaseChangeOut(Game.SecretPhaseState state)
    {
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        CmdSyncSecretState(state);
    }
    public void OnActivePlayerChangeOut(Player.Id state)
    {
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        CmdSyncActivePlayer(state);
    }
    public void UpdateDistribTurn(int turn)
    {
        if (hasBeenSynced)
        {
            hasBeenSynced = false;
            return;
        }
        CmdUpdateDistribTurn(turn);
    }

    public bool AddToken(Field field)
    {
        CWLogging.Instance().LogDebug("PlayerClient::AddToken: Active Player ID: " + activePlayer.ToString());
        if (!game.ActivePlayer().Client.isLocalPlayer)
            return false;

        if( field.Draw() )
        {
            Game.Instance().NextTurn();
            return true;
        }
   
        return false;
    }

    public void SpawnToken(Vector2 position, Color color)
    {
        CmdSpawnToken(position, color);
    }

    public void ClearTokens()
    {
        if (isLocalPlayer)
            CmdClearTokens();
    }

    [Command]
    void CmdSyncGameState(Game.GameState state)
    {
        gameState = state;
    }

    [Command]
    void CmdSyncSecretState(Game.SecretPhaseState state)
    {
        secretPhase = state;
    }

    [Command]
    void CmdSyncActivePlayer(Player.Id id)
    {
        activePlayer = id;
        game.ActivePlayerId().Value = id;
    }

    [Command]
    void CmdSpawnToken(Vector2 position, Color color)
    { 

        var addedToken = Instantiate(token);

        // TODO: Spawn token on server side?
        addedToken.GetComponent<SpriteRenderer>().color = color;
        addedToken.transform.position = position;
        addedTokens.Add(addedToken);
        NetworkServer.Spawn(addedToken);
    }

    [Command]
    void CmdClearTokens()
    {
        foreach (GameObject token in addedTokens)
        {
            Destroy(token);
        }
        addedTokens.Clear();
    }

    [Command]
    void CmdUpdateDistribTurn(int turn)
    {
        distribTurn = turn;
    }
}
