using System.Linq;
using UnityEngine;

/**
 * Created by MW on 03.10.2014.
 */
namespace Engine
{
    public class PlayerList
    {
        public CWState<Player.ID> ActivePlayer { get; private set; }
        public int CurrentNumberOfPlayers { get; private set; }
        private int maxPlayer;
        private Player[] players;

        public PlayerList(int numPlayer)
        {
            CurrentNumberOfPlayers = 0;
            this.maxPlayer = numPlayer;
            players = new Player[numPlayer];
            ActivePlayer = new CWState<Player.ID>();
            ActivePlayer.Value = Player.ID.ILLEGAL;
        }

        public Player CreatePlayer()
        {
            if (CurrentNumberOfPlayers >= maxPlayer)
                return null;
            players[CurrentNumberOfPlayers] = new Player((Player.ID)CurrentNumberOfPlayers + 1); ;

            return players[CurrentNumberOfPlayers++];
        }

        public bool StartGame()
        {
            if (CurrentNumberOfPlayers < 2)
                return false;

            foreach (Player p in players)
            {
                p.isActive = false;
            }

            ActivePlayer.Value = Player.ID.PLAYER1;
            players[0].isActive = true;
            return true;
        }

        public void NextPlayer()
        {
            foreach( Player player in players )
            {
                player.isActive = !player.isActive;
                if (!player.isActive)
                    continue;
                ActivePlayer.Value = player.id;
            }
            CWLogging.Instance().LogDebug(ActivePlayer.Value.ToString());
        }
    }
} //Namespace Engine