using System.Collections.Generic;
using System.Linq;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueGame.Components;
using RogueGame.Fonts;
using RogueGame.GameSystems.Items;
using SadConsole;
using Keyboard = SadConsole.Input.Keyboard;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace RogueGame.Maps
{
    internal class MapScreen : ContainerConsole
    {
        private static readonly Dictionary<Keys, Direction> MovementDirectionMapping = new Dictionary<Keys, Direction>
        {
            { Keys.NumPad7, Direction.UP_LEFT },
            { Keys.NumPad8, Direction.UP },
            { Keys.NumPad9, Direction.UP_RIGHT },
            { Keys.NumPad4, Direction.LEFT },
            { Keys.NumPad6, Direction.RIGHT },
            { Keys.NumPad1, Direction.DOWN_LEFT },
            { Keys.NumPad2, Direction.DOWN },
            { Keys.NumPad3, Direction.DOWN_RIGHT },
            { Keys.Up, Direction.UP },
            { Keys.Down, Direction.DOWN },
            { Keys.Left, Direction.LEFT },
            { Keys.Right, Direction.RIGHT }
        };
        public MovingCastlesMap Map { get; }
        public ScrollingConsole MapRenderer { get; }
        
        public Player Player { get; private set; }

        public MapScreen(int mapWidth, int mapHeight, int viewportWidth, int viewportHeight, Font tileSetFont)
        {
            Map = GenerateDungeon(mapWidth, mapHeight, tileSetFont);

            MapRenderer = Map.CreateRenderer(new XnaRect(0, 0, viewportWidth, viewportHeight), tileSetFont);
            Children.Add(MapRenderer);
            //Map.ControlledGameObject.IsFocused = true;
            IsFocused = true;
            Map.ControlledGameObject.Moved += Player_Moved;
            Map.ControlledGameObjectChanged += ControlledGameObjectChanged;

            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            if (info.IsKeyPressed(Keys.I))
            {
                Window.Message($"Inventory: {string.Join(", ", Player.GetGoRogueComponent<IInventoryComponent>().Items.Select(i => i.Name))}", "Close");
                return true;
            }

            foreach (var key in MovementDirectionMapping.Keys)
            {
                if (info.IsKeyPressed(key))
                {
                    Player.Move(MovementDirectionMapping[key]);
                    return true;
                }
            }
            
            return base.ProcessKeyboard(info);
        }

        private void ControlledGameObjectChanged(object s, ControlledGameObjectChangedArgs e)
        {
            if (e.OldObject != null)
                e.OldObject.Moved -= Player_Moved;

            ((BasicMap) s).ControlledGameObject.Moved += Player_Moved;
        }

        private MovingCastlesMap GenerateDungeon(int width, int height, Font tileSetFont)
        {
            var map = new MovingCastlesMap(width, height);

            var tempMap = new ArrayMap<bool>(map.Width, map.Height);
            QuickGenerators.GenerateRandomRoomsMap(tempMap, maxRooms: 180, roomMinSize: 8, roomMaxSize: 12);
            map.ApplyTerrainOverlay(tempMap, SpawnTerrain);

            Coord posToSpawn;

            for (var i = 0; i < 10; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true);
                var goblin = new BasicEntity(Color.White, Color.Transparent, SpriteAtlas.Goblin, posToSpawn, (int) MapLayer.MONSTERS, isWalkable: false, isTransparent: true);
                goblin.Font = tileSetFont;
                goblin.OnCalculateRenderPosition();
                map.AddEntity(goblin);
            }

            // Spawn a few items
            for (var i = 0; i < 12; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true);

                var item = new BasicEntity(
                    Color.White,
                    Color.Transparent,
                    SpriteAtlas.EtheriumShard,
                    posToSpawn,
                    (int) MapLayer.ITEMS,
                    isWalkable: true,
                    isTransparent: true
                );
                item.AddGoRogueComponent(new PickupableComponent(new InventoryItem("Etherium shard")));
                item.Font = tileSetFont;
                item.OnCalculateRenderPosition();

                map.AddEntity(item);
            }

            // Spawn player
            posToSpawn = map.WalkabilityView.RandomPosition(true);

            Player = new Player(posToSpawn, tileSetFont);
            map.ControlledGameObject = Player;
            map.AddEntity(Player);

            return map;
        }

        private void Player_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);

            var stepTriggers = Map.GetEntities<BasicEntity>(Map.ControlledGameObject.Position)
                .SelectMany(e =>
                {
                    if (!(e is IHasComponents entity))
                    {
                        return new IStepTriggeredComponent[0];
                    }

                    return entity.GetComponents<IStepTriggeredComponent>();
                });

            foreach (var trigger in stepTriggers)
            {
                trigger.OnStep(Map.ControlledGameObject);
            }
        }

        private static IGameObject SpawnTerrain(Coord position, bool mapGenValue)
        {
            // Floor or wall.  This could use the Factory system, or instantiate Floor and Wall classes, or something else if you prefer;
            // this simplistic if-else is just used for example
            if (mapGenValue)
            {
                // Floor
                return new BasicTerrain(Color.White, new Color(61, 35, 50, 255), SpriteAtlas.Ground_Dirt, position, isWalkable: true, isTransparent: true);
            }
            else
            {
                // Wall
                return new BasicTerrain(Color.White, new Color(61, 35, 50, 255), SpriteAtlas.Wall_Brick, position, isWalkable: false, isTransparent: false);
            }
        }
    }
}