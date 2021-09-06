using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Entities;
using RogueGame.Fonts;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame
{
    public sealed class Player : McEntity
    {
        public int FOVRadius;

        public Player(Coord position, Font font)
            : base("Player",
                Color.White,
                Color.Transparent,
                SpriteAtlas.PlayerDefault,
                position,
                (int) MapLayer.PLAYER,
                isWalkable: false,
                isTransparent: true)
        {
            FOVRadius = 10;
            Font = font;
            OnCalculateRenderPosition();
            
            AddGoRogueComponent(new MeleeAttackerComponent(5));
            AddGoRogueComponent(new HealthComponent(100));
            AddGoRogueComponent(new InventoryComponent());
        }
    }
}