using System.Linq;
/**
 * Created by MW on 03.10.2014.
 */
namespace Engine {
public class PlayerList {
    private int currentNumberOfPlayers;
    private int maxPlayer;
    private Player[] players;
	private Player activePlayer;

    public PlayerList(int numPlayer) {
        currentNumberOfPlayers = 0;
        this.maxPlayer = numPlayer;
        players = new Player[numPlayer];
    }

    public void Add(Player player) {
        if( players.Contains(player) || currentNumberOfPlayers >= maxPlayer )
            return;

	    players[currentNumberOfPlayers] = player;
        currentNumberOfPlayers++;
    }

    public int Size() {
        return players.Length;
    }

	public Player.ID ActivePlayer() {
		if( players.Length < 1 ) {
            return Player.ID.ILLEGAL;
        }

		return activePlayer.id;
	}

	public void StartGame() {
		foreach (Player p in players) {
            p.isActive = false;
		}
		activePlayer = players[0];
        activePlayer.isActive = true;
    }

	public void NextPlayer() {
		negateActivityOfAllPlayers();
		activePlayer = findActivePlayer();
	}

	private Player findActivePlayer() {
		foreach(Player p in players){
			if( p.isActive ) {
                return p;
            }
		}

        return null;
	}

	private void negateActivityOfAllPlayers() {
		foreach( Player p in players){
			p.isActive = !p.isActive;
		}
	}
}
} //Namespace Engine