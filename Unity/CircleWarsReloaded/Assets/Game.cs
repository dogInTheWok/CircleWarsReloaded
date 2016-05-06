/**
 * Created by MW on 03.10.2014.
 */
using UnityEngine;
using System.Collections;

namespace Engine
{
    public class Game : Singleton<Game>
    {
        static public int NUM_FIELDS = 12;
        static public int NUM_MAX_PLAYER = 2;
        static public int NUM_SECRETS = 3;
        static public int NUM_FORCES_DISTRIB_PHASE = 14;
        static public int NUM_TURNS_DISTRIB = NUM_MAX_PLAYER * NUM_FORCES_DISTRIB_PHASE;
        static public int NUM_TURNS_SECRET = NUM_MAX_PLAYER * NUM_SECRETS;

        public enum GameState
        {
            NotStarted,
            RunningDistribution,
            RunningSecret,
            EvaluatingFields,
            EvaluatingTotal,
            Terminated
        }

        public enum SecretPhaseState
        {
            NotEntered,
            Batillion,
            Marine,
            Napalm
        }

        public CWState<GameState> CurrentGameState { get; private set; }
        public CWState<SecretPhaseState> CurrentSecretPhaseState { get; private set; }
        public Player.Id Winner { get; private set; }
        public int DistribTurn{ get; set;}

        private PlayerList playerList;
        private FieldList fieldList;
        private int secretTurn = 0;
        
        public Game()
        {
            var factory = GlobalFactory.Instance();
            playerList = factory.createPlayerList();
            fieldList = factory.createFieldList();
            CurrentGameState = new CWState<GameState>();
            CurrentGameState.Value = GameState.NotStarted;
            CurrentSecretPhaseState = new CWState<SecretPhaseState>();
            CurrentSecretPhaseState.Value = SecretPhaseState.NotEntered;
            CurrentGameState.ConnectTo(onGameStateChange);
            DistribTurn = 0;
        }

        public Field CreateField()
        {
            return fieldList.CreateField();
        }
        public Player CreatePlayer(PlayerClient playerClient)
        {
            return playerList.CreatePlayer(playerClient);
        }
        public void Start()
        {
            if (!playerList.Start())
            {
                CWLogging.Instance().LogWarning("Game could not be started. Invalid players.");
                return;
            }

            CurrentGameState.Value = GameState.RunningDistribution;
        }
        public void ClearPlayerTokens()
        {
            foreach (Player p in playerList.Players)
            {
                if (null == p)
                    continue;
                p.Client.ClearTokens();
            }
        }
        public void Reset()
        {
            DistribTurn = 0;
            secretTurn = 0;
            CurrentGameState.Value = GameState.NotStarted;
        }
        

        public CWState<Player.Id> ActivePlayerId()
        {
            return playerList.ActivePlayerId;
        }
        public Player ActivePlayer()
        {
            return playerList.ActivePlayer;
        }

        public void NextTurn()
        {
            if (CurrentGameState.Value == GameState.RunningDistribution)
            {
                nextDistrib();
            }
            else if (CurrentGameState.Value == GameState.RunningSecret)
            {
                nextSecret();
            }

            if (DistribTurn == NUM_TURNS_DISTRIB)
            {
                enterSecretPhase();
            }

            if (secretTurn == NUM_TURNS_SECRET)
            {
                enterEval();
            }
        }
        private void onGameStateChange(Game.GameState state)
        {
            if (GameState.EvaluatingTotal != state)
                return;

            if (fieldList.Score(Player.Id.PLAYER1) == fieldList.Score(Player.Id.PLAYER2))
            {
                Winner = Player.Id.ILLEGAL;
            }
            else
            {
                Winner = fieldList.Score(Player.Id.PLAYER1) > fieldList.Score(Player.Id.PLAYER2) ? Player.Id.PLAYER1 : Player.Id.PLAYER2;
            }
        }
        private void enterEval()
        {
            // Triggers evaluation of every single field.
            CurrentGameState.Value = GameState.EvaluatingFields;

            // Triggers total evaluation
            CurrentGameState.Value = GameState.EvaluatingTotal;

            var logger = CWLogging.Instance();
            logger.LogDebug("Winner");
            logger.LogDebug(Winner.ToString());
            logger.LogDebug(fieldList.Score(Player.Id.PLAYER1).ToString());
            logger.LogDebug(fieldList.Score(Player.Id.PLAYER2).ToString());
            CurrentGameState.Value = GameState.Terminated;
        }
        private void enterSecretPhase()
        {
            CurrentGameState.Value = GameState.RunningSecret;
            CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
        }
        private void nextDistrib()
        {
            playerList.NextPlayer();
            if (!ActivePlayer().Client.isLocalPlayer)
                CWLogging.Instance().LogWarning("Active Player is NOT local!");
            ActivePlayer().Client.UpdateDistribTurn(++DistribTurn);
        }
        private void nextSecret()
        {
            secretTurn++;
            switch (CurrentSecretPhaseState.Value)
            {
                case SecretPhaseState.Batillion:
                    CurrentSecretPhaseState.Value = SecretPhaseState.Marine;
                    break;
                case SecretPhaseState.Marine:
                    CurrentSecretPhaseState.Value = SecretPhaseState.Napalm;
                    break;
                case SecretPhaseState.Napalm:
                    playerList.NextPlayer();
                    CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
                    break;
                default:
                    CWLogging.Instance().LogDebug("Game::NextPhase: default case entered.");
                    CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
                    break;
            }
        }
        
        
    }

} //Namespace Engine
