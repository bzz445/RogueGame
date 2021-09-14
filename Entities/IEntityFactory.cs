using GoRogue;
using RogueGame.GameSystems.Items;
using RogueGame.GameSystems.Player;
using SadConsole;

namespace RogueGame.Entities
{
    public interface IEntityFactory
    {
        McEntity CreateActor(Coord position, ActorTemplate actorTemplate);
        public McEntity CreateItem(Coord position, ItemTemplate itemTemplate);
        Wizard CreatePlayer(Coord position, Player playerInfo);
        Castle CreateCastle(Coord position, Player playerInfo);
    }
}