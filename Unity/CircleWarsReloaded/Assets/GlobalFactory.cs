/**
 * Created by MW on 04.10.2014.
 */
namespace Engine
{
    public class GlobalFactory : Singleton<GlobalFactory>
    {
        public PlayerList createPlayerList()
        {
            return new PlayerList(Game.NUM_MAX_PLAYER);
        }

        public FieldList createFieldList()
        {
            return new FieldList(Game.NUM_FIELDS);
        }
    }
} //Namespace Engine