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
        public int CurrentNumberOfPlayers { get; private set; }
        public Player[] Players { get; private set; }
        private int maxPlayer;
        public CWState<Player.Id> ActivePlayerId { get; private set; }

        public PlayerList(int numPlayer)
        {
            CurrentNumberOfPlayers = 0;
            this.maxPlayer = numPlayer;
            Players = new Player[numPlayer];
            ActivePlayerId = new CWState<Player.Id>();
            ActivePlayerId.Value = Player.Id.ILLEGAL;
            ActivePlayerId.ConnectTo(OnActivePlayerIdChanged);
        }

        public Player CreatePlayer( PlayerClient playerClient)
        {
            if (CurrentNumberOfPlayers >= maxPlayer)
                return null;
            Players[CurrentNumberOfPlayers] = new Player((Player.Id)CurrentNumberOfPlayers + 1, playerClient); ;

            return Players[CurrentNumberOfPlayers++];
        }
        void OnActivePlayerIdChanged(Player.Id id)
        {
            var index = (int)id - 1;
            if (index < 0 || index >= CurrentNumberOfPlayers)
            {
                ActivePlayer = null;
                return;
            }
            ActivePlayer = Players[index];
        }
        public bool StartGame()
        {
            if (CurrentNumberOfPlayers < 2)
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
    }
} //Namespace Engine