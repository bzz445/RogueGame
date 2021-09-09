namespace RogueGame.Maps
{
    public interface IMapFactory
    {
        DungeonMap CreateDungeonMap(int width, int height, IMapPlan mapPlan);
        CastleMap CreateCastleMap(int width, int height, IMapPlan mapPlan);
    }
}