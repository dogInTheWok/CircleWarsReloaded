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
        public bool IsActive { get; set; }
        public Player.ID Owner { get; set; }
        public int FieldId { get; set; }

        public bool IsWon { get; private set; }
        public int TokenCount { get; private set; }

        public bool HasBatillion { get; private set; }
        public bool HasMarine { get; private set; }
        public bool HasNapalm { get; private set; }
        private List<Field> neighbours;
        private Game game;

        public Field(int id)
        {
            Owner = Player.ID.ILLEGAL;
            FieldId = id;
            TokenCount = 0;
            IsActive = true;
            IsWon = false;
            neighbours = new List<Field>();
            game = Game.Instance();
            HasBatillion = false;
            HasMarine = false;
            HasNapalm = false;
        }

        public void addNeighbour( Field neighbour )
        {
            neighbours.Add(neighbour);
        }

        public bool addToken()
        {
            if (Owner == Player.ID.ILLEGAL)
            {
                Owner = game.ActivePlayer().Value;
            } else if (Owner != game.ActivePlayer().Value)
            {
                return false;
            }

            TokenCount = TokenCount + 1;
            return true;
        }

        public bool requestToken()
        {
            return Game.Instance().DispatchForce(this);
        }

        public bool addSecret(Game.SecretPhaseState secretValue)
        {
            switch (secretValue) {
                case Game.SecretPhaseState.Batillion:
                    if (Owner != game.ActivePlayer().Value && Owner != Player.ID.ILLEGAL)
                    {
                        return false;
                    }

                    addToken();
                    HasMarine = true;
                    break;

                case Game.SecretPhaseState.Marine:

                    if (Owner != game.ActivePlayer().Value && Owner != Player.ID.ILLEGAL)
                    {
                        return false;
                    }

                    addToken();
                    addToken();
                    addToken();
                    HasBatillion = true;
                    break;

                case Game.SecretPhaseState.Napalm:
                    if (Owner == game.ActivePlayer().Value)
                    {
                        return false;
                    }

                    IsActive = false;
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
                if (f.Owner == Owner && f.IsActive)
                {
                    evalTokenCount = evalTokenCount + f.TokenCount;
                }
                else if (f.Owner != Player.ID.ILLEGAL && f.IsActive)
                {
                    evalTokenCount = evalTokenCount - f.TokenCount;
                }
            }
            IsWon = evalTokenCount > 0;
        }
    }
} // namespace Engine

