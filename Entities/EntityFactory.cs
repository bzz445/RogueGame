using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.GameSystems.Items;
using RogueGame.Logging;
using SadConsole;

namespace RogueGame.Entities
{
    public class EntityFactory: IEntityFactory
    {
        private readonly Font _font;
        private readonly ILogManager _logManager;
        
        public EntityFactory(Font font, ILogManager logManager)
        {
            _font = font;
            _logManager = logManager;
        }
        
        public BasicEntity CreateActor(int glyph, Coord position, string name)
        {
            var actor = new BasicEntity(Color.White, Color.Transparent, glyph, position, (int)Maps.MapLayer.MONSTERS, isWalkable: false, isTransparent: true)
            {
                Name = name,
            };
            actor.AddGoRogueComponent(new HealthComponent(1));
            actor.AddGoRogueComponent(new SummaryControlComponent());
            
            actor.Font = _font;
            actor.OnCalculateRenderPosition();

            return actor;
        }

        public BasicEntity CreateItem(int glyph, Coord position, string name, string desc)
        {
            var item = new BasicEntity(
                Color.White,
                Color.Transparent,
                glyph,
                position,
                (int)Maps.MapLayer.ITEMS,
                isWalkable: true,
                isTransparent: true)
            {
                Name = name,
            };
            item.AddGoRogueComponent(new SummaryControlComponent());
            item.AddGoRogueComponent(new PickupableComponent(_logManager,
                new InventoryItem(name, desc)));

            // workaround Entity construction bugs by setting font afterward
            item.Font = _font;
            item.OnCalculateRenderPosition();

            return item;
        }
    }
}