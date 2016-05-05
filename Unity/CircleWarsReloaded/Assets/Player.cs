/**
 * Created by MW on 03.10.2014.
 */
namespace Engine
{
    public class Player
    {
        public Player.Id id { get; private set; }
        public bool isActive { get; set; }
        public PlayerClient Client { get; private set; }

        public enum Id
        {
            ILLEGAL,
            PLAYER1,
            PLAYER2
        }


        public Player(Player.Id id, PlayerClient playerClient)
        {
            this.id = id;
            this.Client = playerClient;
        }

    }

} //Namespace Engine