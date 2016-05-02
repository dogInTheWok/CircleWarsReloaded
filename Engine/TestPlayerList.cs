package com.circleSoftwares.unitTests;

import com.circleSoftwares.Game;
import com.circleSoftwares.Player;
import com.circleSoftwares.PlayerList;
import junit.framework.TestCase;
import org.junit.Test;

/**
 * Created by MW on 04.10.2014.
 */
public class TestPlayerList extends TestCase {
	private PlayerList playerList;

	protected void setUp(){
		playerList = new PlayerList(Game.NUM_PLAYER);
	}
	@Test
	public void testAddPlayer(){
		Player player = new Player(Player.ID.PLAYER1);
		playerList.add(player);
		assertEquals(1, playerList.size());
	}
}
