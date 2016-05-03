/**
* Created by MW on 03.10.2014.
*/
using UnityEngine;
using System.Collections;

namespace Engine
{

    public class Field
    {
        public bool IsActive { get; set; }
        public Player.ID Owner { get; set; }

        public bool IsWon { get; private set; }
        public int FieldId { get; private set; }
        public int TokenCount { get; private set; }

        private Field[] neighbours;
        private Game game;

        public Field()
        {
            Owner = Player.ID.ILLEGAL;
            FieldId = -1;
            TokenCount = 0;
            IsActive = true;
            IsWon = false;
            neighbours = new Field[Game.NUM_FIELDS];
            game = Game.Instance();
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

        private void evalField()
        {
            /* determine if field has isWon combat for its owner */

            int evalTokenCount = TokenCount;

            for (int i = 0; i < neighbours.Length; i++)
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

