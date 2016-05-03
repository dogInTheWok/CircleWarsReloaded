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
        static public int NUM_FORCES_DISTRIB_PHASE = 10;
        static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;

        public GameState CurrentState { get; private set;}
        public bool isStarted { get; private set; }
        private int distribTurn;
        private Field.Secret secret;

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
            CurrentState = new GameState();
            CurrentState.Value = GameState.State.NotStarted;
        }

        public void StartGame()
        {
            init();
            playerList.StartGame();
            isStarted = true;
            CurrentState.Value = GameState.State.RunningDistribution;
        }

        private void init()
        {
            fillPlayerList();
            distribTurn = 0;
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
            if (distribTurn == NUM_TURNS_DISTRIB)
            {
                EnterSecretPhase();
            }

            playerList.NextPlayer();

            if (CurrentState.Value == GameState.State.RunningDistribution)
            {
                NextDistrib();
            } else if (CurrentState.Value == GameState.State.RunningSecret)
            {
                NextSecret();
            }
        }

        public bool DispatchForce(Field field)
        {
            if (CurrentState.Value == GameState.State.RunningDistribution)
            {
                if (field.addToken())
                {
                    NextTurn();
                    return true;
                }
                return false;
            } else if (CurrentState.Value == GameState.State.RunningSecret)
            {
                if (field.addSecret(secret))
                {
                    NextTurn();
                }
                return false;
            }
            return false;
        }

        public void EnterSecretPhase()
        {
            CurrentState.Value = GameState.State.RunningSecret;
            return;
        }

        public void NextSecret()
        {
            secret += 1;
        }

        public void NextDistrib()
        {
            distribTurn += 1;
        }
    }

} //Namespace Engine
