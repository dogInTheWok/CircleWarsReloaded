/* /**
 * Created by MW on 03.10.2014.
 */

namespace Engine {

class FieldList {
	Field[] fields;

	public FieldList(int num_fields) {
		fields = new Field[Game.NUM_FIELDS];
		fill();
	}

	private void fill() {
		for(int i = 0; i < Game.NUM_FIELDS; i++)
		{
			fields[i] = new Field();
		}
	}

	public int size() {
		return fields.Length;
	}

	public Field get(int id) {
		return fields[id];
	}
}

} //Namespace Engine