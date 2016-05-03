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

        private List<Field> neighbours;
        private Game game;

        public Field()
        {
            Owner = Player.ID.ILLEGAL;
            FieldId = -1;
            TokenCount = 0;
            IsActive = true;
            IsWon = false;
            neighbours = new List<Field>();
            game = Game.Instance();
        }

        public Field(int id)
        {
            Owner = Player.ID.ILLEGAL;
            FieldId = id;
            TokenCount = 0;
            IsActive = true;
            IsWon = false;
            neighbours = new List<Field>();
            game = Game.Instance();
        }

        public void addNeighbour( Field neighbour )
        {
            neighbours.Add(neighbour);
        }

        public bool addToken()
        {
            if (Owner == Player.ID.ILLEGAL)
            {
                Owner = game.ActivePlayer();
            } else if (Owner != game.ActivePlayer())
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

        public bool addSecret(Game.Secret secret)
        {
            switch (secret) {
                case Game.Secret.marine:
                    addToken();
                    break;
                case Game.Secret.batallion:
                    addToken();
                    addToken();
                    addToken();
                    break;
                case Game.Secret.napalm:
                    IsActive = false;
                    break;
            }

            return true;
        }

        private void evalField()
        {
            /* determine if field has isWon combat for its owner */

            int evalTokenCount = TokenCount;

            for (int i = 0; i < neighbours.Count; i++)
            {
                if (neighbours[i].Owner == Owner)
                {
                    evalTokenCount = evalTokenCount + neighbours[i].TokenCount;
                }
                else
                {
                    evalTokenCount = evalTokenCount - neighbours[i].TokenCount;
                }
            }
            IsWon = evalTokenCount > 0;
        }
    }
} // namespace Engine

