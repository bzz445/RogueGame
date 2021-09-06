using GoRogue;
using RogueGame.GameSystems.Items;
using SadConsole;

namespace RogueGame.Entities
{
    public interface IEntityFactory
    {
        McEntity CreateActor(int glyph, Coord position, string name);
        public McEntity CreateItem(Coord position, ItemTemplate itemTemplate);
        Player CreatePlayer(Coord position);
    }
}