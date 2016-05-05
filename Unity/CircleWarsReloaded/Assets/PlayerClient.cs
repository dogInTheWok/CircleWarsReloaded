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

    [SerializeField]
    private GameObject token;

    private Game game;
    public List<GameObject> addedTokens;
    // The Player representation in Engine.
    private Player player;
    private bool hasBeenSynced;

    // Use this for initialization
    void Start()
    {
        game = Game.Instance();
        player = game.CreatePlayer(this);
        hasBeenSynced = true;
        // Connect to states
        Game.Instance().CurrentGameState.ConnectTo(OnGameStateChangeOut);
        Game.Instance().CurrentSecretPhaseState.ConnectTo(OnSecretPhaseChangeOut);
        Game.Instance().ActivePlayerId().ConnectTo(OnActivePlayerChangeOut);

        CWLogging.Instance().LogDebug(player.id.ToString());
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
        secretPhase = state;
        game.CurrentSecretPhaseState.Value = secretPhase;
    }
    public void OnActivePlayerChangeIn(Player.Id state)
    {
        hasBeenSynced = true;
        activePlayer = state;
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

    public bool AddToken(Field field)
    {
        return activePlayer == player.id ? field.requestToken() : false ;
    }

    public void SpawnToken(Vector2 position, Color color)
    {
        CmdSpawnToken(position, color);
    }

    public void ClearTokens()
    {
        CmdClearTokens();
    }

    [Command]
    void CmdSyncGameState(Game.GameState state)
    {
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
}
