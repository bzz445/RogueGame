using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Fonts;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame.Entities
{
    public class Castle: McEntity
    {
        public int FOVRadius;

        public Castle(Coord position, Font font) 
            : base("Player",
                Color.White,
                Color.Transparent,
                SpriteAtlas.PlayerDefault,
                position,
                (int)DungeonMapLayer.PLAYER,
                isWalkable: false,
                isTransparent: true)
        {
            FOVRadius = 3;
            
            // workaround Entity construction bugs by setting font afterward
            Font = font;
            OnCalculateRenderPosition();
        }
    }
}