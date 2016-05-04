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
        static public int NUM_FORCES_DISTRIB_PHASE = 2;
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

        public CWState<GameState> CurrentGameState { get; private set;  }
        public CWState<SecretPhaseState> CurrentSecretPhaseState { get; private set; }
        public bool isStarted { get; private set; }
        private int distribTurn;
        private int secretTurn;
        public Player.ID winner { get; private set; }

        private PlayerList playerList;
        private FieldList fieldList;

        public Field createField()
        {
            return fieldList.createField();
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
            playerList.StartGame();
            isStarted = true;
            CurrentGameState.Value = GameState.RunningDistribution;
        }

        public void Reset()
        {
            distribTurn = 0;
            secretTurn = 0;
            playerList.StartGame();
            isStarted = true;
            CurrentGameState.Value = GameState.NotStarted;
        }

        private void init()
        {
            fillPlayerList();
            distribTurn = 0;
            secretTurn = 0;
        }

        private void fillPlayerList()
        {
            playerList.Add(new Player(Player.ID.PLAYER1));
            playerList.Add(new Player(Player.ID.PLAYER2));
        }

        public void AddPlayer(Player player)
        {
            playerList.Add(player);
        }

        public int NumPlayer()
        {
            return playerList.Size();
        }

        public int NumFields()
        {
            return fieldList.size();
        }

        public CWState<Player.ID> ActivePlayer()
        {
            return playerList.ActivePlayer;
        }

        public void NextTurn()
        {
            playerList.NextPlayer();

            if (CurrentGameState.Value == GameState.RunningDistribution)
            {
                NextDistrib();
            } else if (CurrentGameState.Value == GameState.RunningSecret)
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
            CWLogging.Instance().LogDebug(CurrentSecretPhaseState.Value.ToString());
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
            } else if (CurrentGameState.Value == GameState.RunningSecret)
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
            switch( CurrentSecretPhaseState.Value )
            {
                case SecretPhaseState.Batillion:
                    CurrentSecretPhaseState.Value = SecretPhaseState.Marine;
                    break;
                case SecretPhaseState.Marine:
                    CurrentSecretPhaseState.Value = SecretPhaseState.Napalm;
                    break;
                default:
                    Debug.Log("WARNING: Game::NextPhase: default case entered.");
                    CurrentSecretPhaseState.Value = SecretPhaseState.Batillion;
                    break;
            }
        }

        public void NextDistrib()
        {
            distribTurn += 1;
        }

        public void EnterEval()
        {
            CurrentGameState.Value = GameState.Evaluating;
            secretTurn = -1;

            fieldList.Eval();
            if (fieldList.Score(Player.ID.PLAYER1) == fieldList.Score(Player.ID.PLAYER2))
            {
                winner = Player.ID.ILLEGAL;
            }
            else
            {
                winner = fieldList.Score(Player.ID.PLAYER1) > fieldList.Score(Player.ID.PLAYER2) ? Player.ID.PLAYER1 : Player.ID.PLAYER2;
            }

            var logger = CWLogging.Instance();
            logger.LogDebug("Winner");
            logger.LogDebug(winner.ToString());
            logger.LogDebug(fieldList.Score(Player.ID.PLAYER1).ToString());
            logger.LogDebug(fieldList.Score(Player.ID.PLAYER2).ToString());
            CurrentGameState.Value = GameState.Terminated;
        }
    }

} //Namespace Engine
