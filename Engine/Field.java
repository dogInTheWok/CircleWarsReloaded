package com.circleSoftwares;

import java.util.ArrayList;

/**
 * Created by MW on 03.10.2014.
 */
public class Field {

	private Player.ID owner;
	private int fieldId;
	private int tokenCount;
	private boolean isActive;
	private boolean isWon;
	private ArrayList<Field> neighbours;

	/*
	public Field(Player.ID ownerId, int newId, int newTokenCount) {
		this.owner = ownerId;
		this.fieldId = newId;
		this.tokenCount = newTokenCount;
	}
	*/

	public Field() {
		this.owner = Player.ID.ILLEGAL;
		this.fieldId = -1;
		this.tokenCount = 0;
		this.isActive = true;
		this.isWon = false;
		this.neighbours = new ArrayList<Field>(Game.NUM_FIELDS);
	}

	public Player.ID getOwner() {
		return owner;
	}

	public int getFieldId() {
		return fieldId;
	}

	public int getTokenCount() {
		return tokenCount;
	}

	public boolean isActive() {
		return isActive;
	}

	public boolean isWon() {
		evalField();
		return isWon;
	}

	public void setOwner(Player.ID newOwner) {
		owner = newOwner;
	}

	public void addToken() {
		tokenCount = tokenCount + 1;
	}

	public void setActive(boolean newState) {
		isActive = newState;
	}

	private void evalField() {
	/* determine if field has isWon combat for its owner */

		int evalTokenCount = tokenCount;

		for (int i = 0; i < neighbours.size(); i++) {
			if (neighbours.get(i).getOwner() == owner) {
				evalTokenCount = evalTokenCount + neighbours.get(i).getTokenCount();
			}
			else{
				evalTokenCount = evalTokenCount - neighbours.get(i).getTokenCount();
			}
		}
		isWon = evalTokenCount > 0;
	}
}
