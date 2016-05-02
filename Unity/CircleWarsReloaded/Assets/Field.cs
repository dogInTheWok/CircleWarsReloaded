/**
* Created by MW on 03.10.2014.
*/

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


        public Field()
        {
            Owner = Player.ID.PLAYER1;
            FieldId = -1;
            TokenCount = 0;
            IsActive = true;
            IsWon = false;
            neighbours = new Field[Game.NUM_FIELDS];
        }

        public void addToken()
        {
            TokenCount = TokenCount + 1;
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

