package com.circleSoftwares.unitTests;

import com.circleSoftwares.Game;
import com.circleSoftwares.GlobalFactory;
import com.circleSoftwares.Player;
import com.sun.xml.internal.bind.v2.model.core.ID;
import junit.framework.TestCase;
import org.junit.Test;

/**
 * Created by MW on 03.10.2014.
 */
public class TestGame extends TestCase{
    private GlobalFactory factory;
	private Game game;


    protected void setUp(){
        factory = new GlobalFactory();
	    game = new Game(factory);

    }

    @Test
    public void testConstructedGameIsNotStarted(){
        assertFalse(game.isStarted());
    }

    @Test
    public void testGameCanBeStarted(){
        game.start();
        assertTrue(game.isStarted());
    }



	@Test
	public void testGameHasFields(){
		assertEquals(Game.NUM_FIELDS, game.numFields());
	}

	@Test
	public void testActivePlayer() throws Exception{
		game.start();
		assertEquals(game.activePlayer(), Player.ID.PLAYER1);
		game.nextTurn();
		assertEquals(game.activePlayer(), Player.ID.PLAYER2);
	}
}
