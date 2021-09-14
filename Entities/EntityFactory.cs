using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Components.AiComponents;
using RogueGame.GameSystems.Items;
using RogueGame.GameSystems.Player;
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

        public McEntity CreateActor(Coord position, ActorTemplate actorTemplate)
        {
            var actor = new McEntity(
                actorTemplate.Name,
                Color.White,
                Color.Transparent,
                actorTemplate.Glyph,
                position,
                (int)Maps.DungeonMapLayer.MONSTERS,
                isWalkable: false,
                isTransparent: true,
                actorTemplate.NameColor);

            actorTemplate.CreateComponents().ForEach(c => actor.AddGoRogueComponent(c));
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
                isTransparent: true,
                Color.DarkGray);
            item.AddGoRogueComponent(new SummaryControlComponent());
            item.AddGoRogueComponent(new PickupableComponent(
                _logManager,
                itemTemplate));

            // workaround Entity construction bugs by setting font afterward
            item.Font = _font;
            item.OnCalculateRenderPosition();

            return item;
        }
        
        public Wizard CreatePlayer(Coord position, Player playerInfo)
        {
            return new Wizard(position, playerInfo, _font);
        }

        public Castle CreateCastle(Coord position, Player playerInfo)
        {
            return new Castle(position, playerInfo, _font);
        }
    }
}