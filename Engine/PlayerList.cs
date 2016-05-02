package com.circleSoftwares;

import java.util.ArrayList;

/**
 * Created by MW on 03.10.2014.
 */
public class PlayerList {
    private int maxPlayer;
    private ArrayList<Player> players;
	private Player activePlayer;

    public PlayerList(int numPlayer) {
        this.maxPlayer = numPlayer;
        players = new ArrayList<Player>(numPlayer);
    }

    public void add(Player player) {
        if( players.contains(player) || players.size() >= maxPlayer )
            return;

	    players.add(player);
    }

    public int size() {
        return players.size();
    }

	public Player.ID activePlayer() throws Exception{
		if( players.size() < 1 )
			throw new Exception();

		return activePlayer.getId();
	}

	public void start() {
		for (Player p : players) {
			p.setActive(false);
		}
		activePlayer = players.get(0);
		activePlayer.setActive(true);
	}

	public void nextPlayer() throws Exception{
		negateActivityOfAllPlayers();
		activePlayer = findActivePlayer();
	}

	private Player findActivePlayer() throws Exception{
		for(Player p : players){
			if( p.isActive() )
				return p;
		}
		throw new Exception();
	}

	private void negateActivityOfAllPlayers() {
		for( Player p : players){
			p.setActive(!p.isActive());
		}
	}
}
