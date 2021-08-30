using System;
using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;

namespace RogueGame.Maps
{
    internal enum MapLayer
    {
        TERRAIN,
        ITEMS,
        MONSTERS,
        PLAYER
    }

    internal class MovingCastlesMap : BasicMap
    {
        public FOVVisibilityHandler FovVisibilityHandler { get; }

        public new Player ControlledGameObject
        {
            get => (Player) base.ControlledGameObject;
            set => base.ControlledGameObject = value;
        }

        public MovingCastlesMap(int width, int height) : base(width, height, Enum.GetNames(typeof(MapLayer)).Length - 1, Distance.CHEBYSHEV,
            entityLayersSupportingMultipleItems: LayerMasker.DEFAULT.Mask((int) MapLayer.ITEMS))
        {
            ControlledGameObjectChanged += ControlledGameObjectTypeCheck<Player>;
            FovVisibilityHandler = new DefaultFOVVisibilityHandler(this, ColorAnsi.BlackBright);
        }
    }
}