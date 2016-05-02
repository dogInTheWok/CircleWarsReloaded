package com.circleSoftwares;

/**
 * Created by MW on 03.10.2014.
 */
public class Player {
	final private Player.ID id;
	private boolean isActive;

	public boolean isActive() {
		return isActive;
	}

	public void setActive(boolean isActive) {
		this.isActive = isActive;
	}

	public enum ID{
		ILLEGAL,
		PLAYER1,
		PLAYER2
	}

	public Player(Player.ID id){
		this.id = id;
	}
	public ID getId() {
		return id;
	}

}
