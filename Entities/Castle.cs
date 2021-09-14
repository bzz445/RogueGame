using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Fonts;
using RogueGame.GameSystems.Player;
using RogueGame.Maps;
using RogueGame.Ui;
using SadConsole;

namespace RogueGame.Entities
{
    public class Castle: McEntity
    {
        public int FOVRadius;

        public Castle(Coord position, Player playerInfo, Font font) 
            : base(playerInfo.Name,
                Color.White,
                Color.Transparent,
                SpriteAtlas.PlayerCastle,
                position,
                (int)DungeonMapLayer.PLAYER,
                isWalkable: false,
                isTransparent: true,
                ColorHelper.PlayerBlue)
        {
            FOVRadius = 3;
            
            // workaround Entity construction bugs by setting font afterward
            Font = font;
            OnCalculateRenderPosition();
            
            AddGoRogueComponent(new InventoryComponent(playerInfo.Items.ToArray()));
            
        }
    }
}