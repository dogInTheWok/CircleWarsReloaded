/**
 * Created by MW on 03.10.2014.
 */
using UnityEngine;
using System.Collections;

namespace Engine
{
    public class GameSingleton
    {
        static public Game Instance()
        {
            if (null == s_instance)
            {
                s_instance = new Game(new GlobalFactory());
            }

            return s_instance;
        }
        static private Game s_instance;
    }

    public class Game : GameSingleton
    {
        static public int NUM_FIELDS = 12;
        static public int NUM_PLAYER = 2;
        static public int NUM_SECRETS = 3;
        static public int NUM_FORCES_DISTRIB_PHASE = 14;
        static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;
        static public int NUM_TURNS_SECRET = NUM_PLAYER * NUM_SECRETS;

        public GameState CurrentGameState { get; private set;}
        public SecretPhaseState CurrentSecretPhaseState { get; private set; }
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

        public Game(GlobalFactory factory)
        {
            playerList = factory.createPlayerList();
            fieldList = factory.createFieldList();
            isStarted = false;
            CurrentGameState = new GameState();
            CurrentGameState.Value = GameState.State.NotStarted;
            CurrentSecretPhaseState = new SecretPhaseState();
            CurrentSecretPhaseState.Value = SecretPhaseState.State.NotEntered;
        }

        public void StartGame()
        {
            init();
            playerList.StartGame();
            isStarted = true;
            CurrentGameState.Value = GameState.State.RunningDistribution;
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

        public Player.ID ActivePlayer()
        {
            return playerList.ActivePlayer();
        }

        public void NextTurn()
        {
            playerList.NextPlayer();

            if (CurrentGameState.Value == GameState.State.RunningDistribution)
            {
                NextDistrib();
            } else if (CurrentGameState.Value == GameState.State.RunningSecret)
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
            Debug.Log(CurrentSecretPhaseState.Value);
        }

        public bool DispatchForce(Field field)
        {
            if (CurrentGameState.Value == GameState.State.RunningDistribution)
            {
                if (field.addToken())
                {
                    NextTurn();
                    return true;
                }
                return false;
            } else if (CurrentGameState.Value == GameState.State.RunningSecret)
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
            CurrentGameState.Value = GameState.State.RunningSecret;
            CurrentSecretPhaseState.Value = SecretPhaseState.State.Batillion;
            distribTurn = -1;
            return;
        }

        public void NextSecret()
        {
            secretTurn += 1;
            switch( CurrentSecretPhaseState.Value )
            {
                case SecretPhaseState.State.Batillion:
                    CurrentSecretPhaseState.Value = SecretPhaseState.State.Marine;
                    break;
                case SecretPhaseState.State.Marine:
                    CurrentSecretPhaseState.Value = SecretPhaseState.State.Napalm;
                    break;
                default:
                    Debug.Log("WARNING: Game::NextPhase: default case entered.");
                    CurrentSecretPhaseState.Value = SecretPhaseState.State.Batillion;
                    break;
            }
        }

        public void NextDistrib()
        {
            distribTurn += 1;
        }

        public void EnterEval()
        {
            CurrentGameState.Value = GameState.State.Evaluating;
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

            Debug.Log("Winner");
            Debug.Log(winner);
            Debug.Log(fieldList.Score(Player.ID.PLAYER1));
            Debug.Log(fieldList.Score(Player.ID.PLAYER2));
            CurrentGameState.Value = GameState.State.Terminated;
        }
    }

} //Namespace Engine
