/**
 * Created by MW on 03.10.2014.
 */
namespace Engine
{
    public class Player
    {
        public enum Id
        {
            ILLEGAL,
            PLAYER1,
            PLAYER2
        }
        public Id PlayerId { get; private set; }
        public PlayerClient Client { get; private set; }

        public Player(Id id, PlayerClient playerClient)
        {
            PlayerId = id;
            Client = playerClient;
        }

    }

} //Namespace Engine