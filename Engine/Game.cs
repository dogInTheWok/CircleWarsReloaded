package com.circleSoftwares;

/**
 * Created by MW on 03.10.2014.
 */
public class Game {
	static public int NUM_FIELDS = 12;
    static public int NUM_PLAYER = 2;
	static public int NUM_FORCES_DISTRIB_PHASE = 10;
	static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;

    private boolean isStarted;

	private int distribTurn;

    private PlayerList playerList;
	private FieldList fieldList;

    public Game(GlobalFactory factory){
        playerList = factory.createPlayerList();
	    fieldList = factory.createFieldList();
        isStarted = false;
    }
    public boolean isStarted(){
        return isStarted;
    }

    public void start() {
	    init();
	    playerList.start();
	    isStarted = true;
    }

	private void init() {
		fillPlayerList();
		distribTurn = 0;
	}

	private void fillPlayerList() {
		Player player1 = new Player(Player.ID.PLAYER1);
		Player player2 = new Player(Player.ID.PLAYER2);
		playerList.add(player1);
		playerList.add(player2);
	}

	public void addPlayer(Player player){
        playerList.add(player);
    }

    public int numPlayer() {
        return playerList.size();
    }

	public int numFields() {
		return fieldList.size();
	}

	public Player.ID activePlayer() throws Exception{
		return playerList.activePlayer();
	}

	public void nextTurn() throws Exception{
		playerList.nextPlayer();
		distribTurn = distribTurn++;
	}

	public void dispatchForce(Field field) throws Exception {
		field.addToken();
		nextTurn();
	}
}
