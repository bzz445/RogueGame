using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Components.AiComponents;
using RogueGame.GameSystems.Items;
using RogueGame.Logging;
using SadConsole;

namespace RogueGame.Entities
{
    public class EntityFactory : IEntityFactory
    {
        private readonly Font _font;
        private readonly ILogManager _logManager;

        public EntityFactory(Font font, ILogManager logManager)
        {
            _font = font;
            _logManager = logManager;
        }

        public McEntity CreateActor(int glyph, Coord position, string name)
        {
            var actor = new McEntity(name, Color.White, Color.Transparent, glyph, position, (int)Maps.DungeonMapLayer.MONSTERS, isWalkable: false, isTransparent: true);

            actor.AddGoRogueComponent(new WalkAtPlayerAiComponent(6));
            actor.AddGoRogueComponent(new MeleeAttackerComponent(5));
            actor.AddGoRogueComponent(new HealthComponent(10));
            actor.AddGoRogueComponent(new SummaryControlComponent());

            // workaround Entity construction bugs by setting font afterward
            actor.Font = _font;
            actor.OnCalculateRenderPosition();

            return actor;
        }

        public McEntity CreateItem(Coord position, ItemTemplate itemTemplate)
        {
            var item = new McEntity(
                itemTemplate.Name,
                Color.White,
                Color.Transparent,
                itemTemplate.Glyph,
                position,
                (int)Maps.DungeonMapLayer.ITEMS,
                isWalkable: true,
                isTransparent: true);
            item.AddGoRogueComponent(new SummaryControlComponent());
            item.AddGoRogueComponent(new PickupableComponent(
                _logManager,
                new InventoryItem(itemTemplate)));

            // workaround Entity construction bugs by setting font afterward
            item.Font = _font;
            item.OnCalculateRenderPosition();

            return item;
        }
        
        public Player CreatePlayer(Coord position)
        {
            return new Player(position, _font);
        }

        public Castle CreateCastle(Coord position)
        {
            return new Castle(position, _font);
        }
    }
}