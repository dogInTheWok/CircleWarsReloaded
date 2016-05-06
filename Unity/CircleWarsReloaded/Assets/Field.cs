/**
* Created by MW on 03.10.2014.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Engine
{

    public class Field
    {
        public Player.Id Owner { get; set; }
        public bool IsWon { get; private set; }
        public bool HasBatillion { get; private set; }
        public bool HasMarine { get; private set; }
        public bool HasNapalm { get; private set; }

        private Game game;
        private List<Field> neighbours = new List<Field>();
        private int TokenCount = 0;

        public Field()
        {
            Owner = Player.Id.ILLEGAL;
            IsWon = false;
            HasBatillion = false;
            HasMarine = false;
            HasNapalm = false;

            game = Game.Instance();
            game.CurrentGameState.ConnectTo(OnStateChange);
        }
            
        public void OnStateChange(Game.GameState state)
        {
            switch (state)
            {
                case Game.GameState.NotStarted:
                    Reset();
                    break;
                case Game.GameState.EvaluatingFields:
                    evalField();
                    break;
                default:
                    break;
            }
        }

        public void AddNeighbour( Field neighbour )
        {
            neighbours.Add(neighbour);
        }
        public bool addToken()
        {
            if (Owner == Player.Id.ILLEGAL)
            {
                Owner = game.ActivePlayerId().Value;
            } else if (Owner != game.ActivePlayerId().Value)
            {
                return false;
            }

            TokenCount = TokenCount + 1;
            return true;
        }

        public bool AddToken()
        {
            if (game.CurrentGameState.Value == Game.GameState.RunningDistribution)
            {
                return addToken();
            }
            else if (game.CurrentGameState.Value == Game.GameState.RunningSecret)
            {
                return addSecret(game.CurrentSecretPhaseState.Value);
            }
            return false;
        }

        public bool addSecret(Game.SecretPhaseState secretValue)
        {
            switch (secretValue) {
                case Game.SecretPhaseState.Batillion:
                    if (Owner != game.ActivePlayerId().Value && Owner != Player.Id.ILLEGAL)
                    {
                        return false;
                    }

                    addToken();
                    HasMarine = true;
                    break;

                case Game.SecretPhaseState.Marine:

                    if (Owner != game.ActivePlayerId().Value && Owner != Player.Id.ILLEGAL)
                    {
                        return false;
                    }

                    addToken();
                    addToken();
                    addToken();
                    HasBatillion = true;
                    break;

                case Game.SecretPhaseState.Napalm:
                    if (Owner == game.ActivePlayerId().Value)
                    {
                        return false;
                    }

                    HasNapalm = true;
                    break;
            }

            return true;
        }

        public void evalField()
        {
            /* determine if field has isWon combat for its owner */

            int evalTokenCount = TokenCount;

            foreach (Field f in neighbours)
            {
                if (f.Owner == Owner && !f.HasNapalm)
                {
                    evalTokenCount = evalTokenCount + f.TokenCount;
                }
                else if (f.Owner != Player.Id.ILLEGAL && !f.HasNapalm)
                {
                    evalTokenCount = evalTokenCount - f.TokenCount;
                }
            }
            IsWon = evalTokenCount > 0;
        }

        public void Reset()
        {
            Owner = Player.Id.ILLEGAL;
            TokenCount = 0;
            IsWon = false;
            HasBatillion = false;
            HasMarine = false;
            HasNapalm = false;
        }
    }
} // namespace Engine

