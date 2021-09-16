using GoRogue;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Fonts;
using RogueGame.GameSystems.Player;
using RogueGame.GameSystems.Spells;
using RogueGame.Maps;
using RogueGame.Ui;
using SadConsole;

namespace RogueGame.Entities
{
    public sealed class Wizard : McEntity
    {
        public int FOVRadius;

        public Wizard(Coord position, Player playerInfo, Font font)
            : base(playerInfo.Name,
                Color.White,
                Color.Transparent,
                SpriteAtlas.PlayerDefault,
                position,
                (int) DungeonMapLayer.PLAYER,
                isWalkable: false,
                isTransparent: true,
                ColorHelper.PlayerBlue)
        {
            FOVRadius = 10;
            Font = font;
            OnCalculateRenderPosition();
            
            AddGoRogueComponent(new MeleeAttackerComponent(5));
            AddGoRogueComponent(new SpellCastingComponent(
                SpellAtlas.ConjureFlame,
                SpellAtlas.RayOfFrost,
                SpellAtlas.EtherealStep));
            AddGoRogueComponent(new HealthComponent(playerInfo.MaxHealth, playerInfo.Health));
            AddGoRogueComponent(new InventoryComponent(playerInfo.Items.ToArray()));
        }
    }
}