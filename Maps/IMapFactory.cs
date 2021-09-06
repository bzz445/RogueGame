namespace RogueGame.Maps
{
    public interface IMapFactory
    {
        MovingCastlesMap Create(int width, int height, IMapPlan mapPlan);
    }
}