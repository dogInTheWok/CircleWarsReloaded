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
        private int tokenCount = 0;

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
        public bool Draw()
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

        private bool addSecret(Game.SecretPhaseState secretValue)
        {
            switch (secretValue)
            {
                case Game.SecretPhaseState.Batillion:
                    if (Owner != game.ActivePlayerId().Value && Owner != Player.Id.ILLEGAL)
                        return false;
     
                    addToken();
                    HasMarine = true;
                    break;

                case Game.SecretPhaseState.Marine:
                    if (Owner != game.ActivePlayerId().Value && Owner != Player.Id.ILLEGAL)
                        return false;

                    addToken();
                    addToken();
                    addToken();
                    HasBatillion = true;
                    break;

                case Game.SecretPhaseState.Napalm:
                    if (Owner == game.ActivePlayerId().Value)
                        return false;

                    HasNapalm = true;
                    break;
                default:
                    return false;
            }

            return true;
        }
        private bool addToken()
        {
            if (Owner == Player.Id.ILLEGAL)
            {
                Owner = game.ActivePlayerId().Value;
            } else if (Owner != game.ActivePlayerId().Value)
            {
                return false;
            }

            tokenCount++;
            return true;
        }
        private void evalField()
        {
            if( HasNapalm )
            {
                IsWon = false;
                return;
            }

            // Determine if field has isWon combat for its owner
            int evalTokenCount = tokenCount;
            foreach (Field f in neighbours)
            {
                if (f.Owner == Owner && !f.HasNapalm)
                {
                    evalTokenCount = evalTokenCount + f.tokenCount;
                }
                else if (f.Owner != Player.Id.ILLEGAL && !f.HasNapalm)
                {
                    evalTokenCount = evalTokenCount - f.tokenCount;
                }
            }
            IsWon = evalTokenCount > 0;
        }
        private void reset()
        {
            Owner = Player.Id.ILLEGAL;
            tokenCount = 0;
            IsWon = false;
            HasBatillion = false;
            HasMarine = false;
            HasNapalm = false;
        }
    }
} // namespace Engine

