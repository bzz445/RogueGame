using RogueGame.GameSystems.Player;

namespace RogueGame.Maps
{
    public interface IMapFactory
    {
        DungeonMap CreateDungeonMap(int width, int height, IMapPlan mapPlan, Player playerInfo);
        CastleMap CreateCastleMap(int width, int height, IMapPlan mapPlan, Player playerInfo);
    }
}