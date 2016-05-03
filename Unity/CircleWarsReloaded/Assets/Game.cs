/**
 * Created by MW on 03.10.2014.
 */
using UnityEngine;
using System.Collections;

namespace Engine {

public class Game {
	static public int NUM_FIELDS = 12;
    static public int NUM_PLAYER = 2;
	static public int NUM_FORCES_DISTRIB_PHASE = 10;
	static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;

    static public Game Instance() {
            if( null == s_instance )
            {
                s_instance = new Game( new GlobalFactory() );
                s_instance.StartGame();
            }

            return s_instance;
    }

    static private Game s_instance;

    public bool isStarted { get; private set; }
	private int distribTurn;

    private PlayerList playerList;
	private FieldList fieldList;
    
    public Field createField() {
            return fieldList.createField();
    }

    public Game(GlobalFactory factory){
        playerList = factory.createPlayerList();
	    fieldList = factory.createFieldList();
        isStarted = false;
    }

    public void StartGame() {
	    init();
	    playerList.StartGame();
	    isStarted = true;
    }

	private void init() {
		fillPlayerList();
		distribTurn = 0;
	}

	private void fillPlayerList() {
		playerList.Add(new Player(Player.ID.PLAYER1));
		playerList.Add(new Player(Player.ID.PLAYER2));
	}

	public void AddPlayer(Player player){
        playerList.Add(player);
    }

    public int NumPlayer() {
        return playerList.Size();
    }

	public int NumFields() {
		return fieldList.size();
	}

	public Player.ID ActivePlayer() {
		return playerList.ActivePlayer();
	}

	public void NextTurn() {
		playerList.NextPlayer();
		distribTurn = distribTurn++;
	}

	public void DispatchForce(Field field) {
		field.addToken();
		NextTurn();
	}
}

} //Namespace Engine