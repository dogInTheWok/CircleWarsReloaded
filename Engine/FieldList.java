package com.circleSoftwares;

import java.util.ArrayList;

/**
 * Created by MW on 03.10.2014.
 */
public class FieldList {
	ArrayList<Field> fields;

	public FieldList(int num_fields) {
		fields = new ArrayList<Field>(Game.NUM_FIELDS);
		fill();
	}

	private void fill() {
		for(int i = 0; i < Game.NUM_FIELDS; i++)
		{
			fields.add(new Field());
		}
	}

	public int size() {
		return fields.size();
	}

	public Field get(int id) {
		return fields.get(id);
	}
}
