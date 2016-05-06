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
        static public int NUM_PLAYER = 2;
        static public int NUM_SECRETS = 3;
        static public int NUM_FORCES_DISTRIB_PHASE = 14;
        static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;
        static public int NUM_TURNS_SECRET = NUM_PLAYER * NUM_SECRETS;

        public enum GameState
        {
            NotStarted,
            RunningDistribution,
            RunningSecret,
            Evaluating,
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
        public bool isStarted { get; private set; }
        private int distribTurn;
        private int secretTurn;
        public Player.Id winner { get; private set; }

        private PlayerList playerList;
        private FieldList fieldList;

        public Field CreateField()
        {
            return fieldList.createField();
        }

        public Player CreatePlayer(PlayerClient playerClient)
        {
            return playerList.CreatePlayer(playerClient);
        }

        public Game()
        {
            var factory = GlobalFactory.Instance();
            playerList = factory.createPlayerList();
            fieldList = factory.createFieldList();
            isStarted = false;
            CurrentGameState = new CWState<GameState>();
            CurrentGameState.Value = GameState.NotStarted;
            CurrentSecretPhaseState = new CWState<SecretPhaseState>();
            CurrentSecretPhaseState.Value = SecretPhaseState.NotEntered;
            init();
        }

        public void StartGame()
        {
            if (!playerList.StartGame())
            {
                CWLogging.Instance().LogWarning("Game could not be started. Invalid players.");
                return;
            }

            isStarted = true;
            CurrentGameState.Value = GameState.RunningDistribution;
        }

        public void Clear()
        {
            foreach( Player p in playerList.Players )
            {
                if (null == p)
                    continue;
                p.Client.ClearTokens();
            }
        }
        public void Reset()
        {
            distribTurn = 0;
            secretTurn = 0;
            CurrentGameState.Value = GameState.NotStarted;
        }

        private void init()
        {
            distribTurn = 0;
            secretTurn = 0;
        }

        public int NumFields()
        {
            return fieldList.size();
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
                NextDistrib();
            }
            else if (CurrentGameState.Value == GameState.RunningSecret)
            {
                NextSecret();
            }

            if (distribTurn == NUM_TURNS_DISTRIB)
            {
                EnterSecretPhase();
            }

            if (secretTurn == NUM_TURNS_SECRET)
            {
                EnterEval();
            }
        }

        public bool DispatchForce(Field field)
        {
            if (CurrentGameState.Value == GameState.RunningDistribution)
            {
                if (field.addToken())
                {
                    NextTurn();
                    return true;
                }
                return false;
            }
            else if (CurrentGameState.Value == GameState.RunningSecret)
            {
                if (field.addSecret(CurrentSecretPhaseState.Value))
                {
                    NextTurn();
                }
                return false;
            }
            return false;
        }

        public void EnterSecretPhase()
        {
            CurrentGameState.Value = GameState.RunningSecret;
            CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
            distribTurn = -1;
            return;
        }

        public void NextSecret()
        {
            secretTurn += 1;
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
                    Debug.Log("WARNING: Game::NextPhase: default case entered.");
                    CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
                    break;
            }
        }

        public void NextDistrib()
        {
            playerList.NextPlayer();
            distribTurn += 1;
        }

        public void EnterEval()
        {
            CurrentGameState.Value = GameState.Evaluating;
            secretTurn = -1;

            fieldList.Eval();
            if (fieldList.Score(Player.Id.PLAYER1) == fieldList.Score(Player.Id.PLAYER2))
            {
                winner = Player.Id.ILLEGAL;
            }
            else
            {
                winner = fieldList.Score(Player.Id.PLAYER1) > fieldList.Score(Player.Id.PLAYER2) ? Player.Id.PLAYER1 : Player.Id.PLAYER2;
            }

            var logger = CWLogging.Instance();
            logger.LogDebug("Winner");
            logger.LogDebug(winner.ToString());
            logger.LogDebug(fieldList.Score(Player.Id.PLAYER1).ToString());
            logger.LogDebug(fieldList.Score(Player.Id.PLAYER2).ToString());
            CurrentGameState.Value = GameState.Terminated;
        }
    }

} //Namespace Engine
