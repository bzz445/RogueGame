using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using RogueGame.Fonts;
using SadConsole;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace RogueGame.Maps
{
    internal class MapScreen : ContainerConsole
    {
        public MovingCastlesMap Map { get; }
        public ScrollingConsole MapRenderer { get; }

        public MapScreen(int mapWidth, int mapHeight, int viewportWidth, int viewportHeight, Font tileSetFont)
        {
            Map = GenerateDungeon(mapWidth, mapHeight, tileSetFont);

            MapRenderer = Map.CreateRenderer(new XnaRect(0, 0, viewportWidth, viewportHeight), tileSetFont);
            Children.Add(MapRenderer);
            Map.ControlledGameObject.IsFocused = true;
            Map.ControlledGameObject.Moved += Player_Moved;
            Map.ControlledGameObjectChanged += ControlledGameObjectChanged;

            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        private void ControlledGameObjectChanged(object s, ControlledGameObjectChangedArgs e)
        {
            if (e.OldObject != null)
                e.OldObject.Moved -= Player_Moved;

            ((BasicMap) s).ControlledGameObject.Moved += Player_Moved;
        }

        private static MovingCastlesMap GenerateDungeon(int width, int height, Font tileSetFont)
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

            // Spawn player
            posToSpawn = map.WalkabilityView.RandomPosition(true);

            var player = new Player(posToSpawn, tileSetFont);
            map.ControlledGameObject = player;
            map.AddEntity(player);

            return map;
        }

        private void Player_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
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