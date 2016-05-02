/**
 * Created by MW on 03.10.2014.
 */
public class Player {
	public Player.ID id { get; private set; }
	public bool isActive { get; set; }

	public enum ID{
		ILLEGAL,
		PLAYER1,
		PLAYER2
	}

	public Player(Player.ID id){
		this.id = id;
	}
}
