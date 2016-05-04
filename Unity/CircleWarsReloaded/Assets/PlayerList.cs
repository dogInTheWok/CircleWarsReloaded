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
        private int currentNumberOfPlayers;
        private int maxPlayer;
        private Player[] players;

        public PlayerList(int numPlayer)
        {
            currentNumberOfPlayers = 0;
            this.maxPlayer = numPlayer;
            players = new Player[numPlayer];
            ActivePlayer = new CWState<Player.ID>();
            ActivePlayer.Value = Player.ID.ILLEGAL;
        }

        public void Add(Player player)
        {
            if (players.Contains(player) || currentNumberOfPlayers >= maxPlayer)
                return;
            players[currentNumberOfPlayers] = player;
            currentNumberOfPlayers++;
        }

        public int Size()
        {
            return players.Length;
        }

        public void StartGame()
        {
            foreach (Player p in players)
            {
                p.isActive = false;
            }

            ActivePlayer.Value = Player.ID.PLAYER1;
            players[0].isActive = true;
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