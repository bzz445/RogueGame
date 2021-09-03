using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Fonts;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame
{
    internal class Player : BasicEntity
    {
        public int FOVRadius;

        public Player(Coord position, Font font)
            : base(Color.White,
                Color.Transparent,
                SpriteAtlas.PlayerDefault,
                position,
                (int) MapLayer.PLAYER,
                isWalkable: false,
                isTransparent: true)
        {
            FOVRadius = 10;
            Name = "Player";
            Font = font;
            OnCalculateRenderPosition();
            AddGoRogueComponent(new HealthComponent(1));
            AddGoRogueComponent(new InventoryComponent());
        }

        public void Move(Direction direction)
        {
            Position += direction;
        }
    }
}