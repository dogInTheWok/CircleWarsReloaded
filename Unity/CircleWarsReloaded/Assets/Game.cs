/**
 * Created by MW on 03.10.2014.
 */
namespace Engine {

public class Game {
	static public int NUM_FIELDS = 12;
    static public int NUM_PLAYER = 2;
	static public int NUM_FORCES_DISTRIB_PHASE = 10;
	static public int NUM_TURNS_DISTRIB = NUM_PLAYER * NUM_FORCES_DISTRIB_PHASE;

    public bool isStarted { get; private set; }
	private int distribTurn;

    private PlayerList playerList;
	private FieldList fieldList;

    public Game(GlobalFactory factory){
        playerList = factory.createPlayerList();
	    fieldList = factory.createFieldList();
        isStarted = false;
    }

    public void Start() {
	    init();
	    playerList.Start();
	    isStarted = true;
    }

	private void init() {
		fillPlayerList();
		distribTurn = 0;
	}

	private void fillPlayerList() {
		Player player1 = new Player(Player.ID.PLAYER1);
		Player player2 = new Player(Player.ID.PLAYER2);
		playerList.Add(player1);
		playerList.Add(player2);
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