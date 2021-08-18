using System.Collections.Generic;
using GoRogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame
{
    public class Player : BasicEntity
    {
        public int FOVRadius;

        private static readonly Dictionary<Keys, Direction> SMovementDirectionMapping = new Dictionary<Keys, Direction>
        {
            {Keys.NumPad7, Direction.UP_LEFT},
            {Keys.NumPad8, Direction.UP},
            {Keys.NumPad9, Direction.UP_RIGHT},
            {Keys.NumPad4, Direction.LEFT},
            {Keys.NumPad6, Direction.RIGHT},
            {Keys.NumPad1, Direction.DOWN_LEFT},
            {Keys.NumPad2, Direction.DOWN},
            {Keys.NumPad3, Direction.DOWN_RIGHT},
            {Keys.Up, Direction.UP},
            {Keys.Down, Direction.DOWN},
            {Keys.Left, Direction.LEFT},
            {Keys.Right, Direction.RIGHT}
        };

        public Player(Coord position): this(position, Global.FontDefault) {}
        
        public Player(Coord position, Font font): 
            base(
                Color.White, 
                Color.Black, 
                '@', 
                position, 
                (int) MapLayer.PLAYER,
                false,
                true)
        {
            FOVRadius = 10;
            Font = font;
        }


        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            Direction moveDirection = Direction.NONE;
            foreach (var key in SMovementDirectionMapping.Keys)
            {
                if (info.IsKeyPressed(key))
                {
                    moveDirection = SMovementDirectionMapping[key];
                    break;
                }
            }

            Position += moveDirection;

            if (moveDirection != Direction.NONE)
                return true;
            else
                return base.ProcessKeyboard(info);
        }
    }
}