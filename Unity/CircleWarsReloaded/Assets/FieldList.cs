/* /**
 * Created by MW on 03.10.2014.
 */

namespace Engine
{
    public class FieldList
    {
        private Field[] fields;
        private int currentNumberOfFields = 0;

        public FieldList(int num_fields)
        {
            fields = new Field[num_fields];
        }

        public Field CreateField()
        {
            if (currentNumberOfFields >= fields.Length)
                return null;

            fields[currentNumberOfFields] = new Field();
            return fields[currentNumberOfFields++];
        }
        public int Score(Player.Id playerId)
        {
            int score = 0;
            foreach (Field f in fields)
            {
                if (f.IsWon && f.Owner == playerId)
                    score++;
            }

            return score;
        }
    }

} //Namespace Engine