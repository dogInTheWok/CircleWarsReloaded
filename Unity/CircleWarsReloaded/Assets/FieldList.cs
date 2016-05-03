/* /**
 * Created by MW on 03.10.2014.
 */

namespace Engine {

public class FieldList {
	private Field[] fields;
    private int currentNumberOfFields;

	public FieldList(int num_fields) {
		fields = new Field[Game.NUM_FIELDS];
        currentNumberOfFields = 0;
	}

    public Field createField()
    {
            if( currentNumberOfFields >= Game.NUM_FIELDS )
            {
                return null;
            }

            fields[currentNumberOfFields] = new Field(currentNumberOfFields);
            currentNumberOfFields++;
            return fields[currentNumberOfFields - 1];
    }

	public int size() {
		return fields.Length;
	}

	public Field get(int id) {
		return fields[id];
	}

    public void Eval()
        {
            foreach (Field f in fields)
            {
                f.evalField();
            }
        }

        public int Score(Player.ID playerId)
        {
            int score = 0;

            foreach (Field f in fields)
            {
                if (f.IsActive && f.IsWon)
                    score++;
            }

            return score;
        }
}

} //Namespace Engine