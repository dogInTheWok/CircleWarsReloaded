using System.Linq;
using UnityEngine;

/**
 * Created by MW on 03.10.2014.
 */
namespace Engine
{
    public class PlayerList
    {
        public Player ActivePlayer { get; private set; }
        public CWState<Player.Id> ActivePlayerId { get; private set; }
        public Player[] Players { get; private set; }

        private int currentNumberOfPlayers = 0;

        public PlayerList(int numPlayer)
        {
            Players = new Player[numPlayer];
            ActivePlayerId = new CWState<Player.Id>();
            ActivePlayerId.Value = Player.Id.ILLEGAL;
            ActivePlayerId.ConnectTo(onActivePlayerIdChanged);
        }

        public Player CreatePlayer( PlayerClient playerClient)
        {
            if (currentNumberOfPlayers >= Players.Length)
                return null;
            Players[currentNumberOfPlayers] = new Player((Player.Id)currentNumberOfPlayers + 1, playerClient); ;

            return Players[currentNumberOfPlayers++];
        }
        
        public bool Start()
        {
            if (currentNumberOfPlayers < Players.Length)
                return false;

            foreach (Player p in Players)
            {
                p.isActive = false;
            }

            ActivePlayerId.Value = Player.Id.PLAYER1;
            Players[0].isActive = true;
            return true;
        }
        public void NextPlayer()
        {
            foreach( Player player in Players )
            {
                player.isActive = !player.isActive;
                if (!player.isActive)
                    continue;
                ActivePlayerId.Value = player.id;
            }

            CWLogging.Instance().LogDebug("Currently active player: " + ActivePlayerId.Value.ToString());
        }

        private void onActivePlayerIdChanged(Player.Id id)
        {
            var index = (int)id - 1;
            if (index < 0 || index >= currentNumberOfPlayers)
            {
                ActivePlayer = null;
                return;
            }
            ActivePlayer = Players[index];
        }
    }
} //Namespace Engine