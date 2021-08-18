using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using SadConsole;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace RogueGame.Maps
{
    public class MapScreen : ContainerConsole
    {
        public RogueGameMap Map { get; }
        public ScrollingConsole MapRenderer { get; }

        public MapScreen(int mapWidth, int mapHeight, int viewportWidth, int viewportHeight)
        {
            Map = GenerateDungeon(mapWidth, mapHeight);
            MapRenderer = Map.CreateRenderer(new XnaRect(0, 0, viewportWidth, viewportHeight),
                SadConsole.Global.FontDefault);
            Children.Add(MapRenderer);
            Map.ControlledGameObject.IsFocused = true;
            Map.ControlledGameObject.Moved += Player_Moved!;
            Map.ControlledGameObjectChanged += ControlledGameObjectChanged!;
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        private void ControlledGameObjectChanged(object s, ControlledGameObjectChangedArgs e)
        {
            if (e.OldObject != null)
                e.OldObject.Moved -= Player_Moved!;

            ((BasicMap) s).ControlledGameObject.Moved += Player_Moved!;
        }

        private static RogueGameMap GenerateDungeon(int width, int height)
        {
            var map = new RogueGameMap(width, height);
            var tempMap = new ArrayMap<bool>(map.Width, map.Height);
            //QuickGenerators.GenerateDungeonMazeMap(tempMap, minRooms: 10, maxRooms: 20, roomMinSize: 5, roomMaxSize: 11);
            QuickGenerators.GenerateRandomRoomsMap(tempMap, maxRooms: 180, roomMinSize: 8, roomMaxSize: 12);
            map.ApplyTerrainOverlay(tempMap, SpawnTerrain);

            Coord posToSpawn;
            for (var i = 0; i < 10; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true);
                var goblin = new BasicEntity(Color.Red, Color.Transparent, 'g', posToSpawn, (int) MapLayer.MONSTERS,
                    isWalkable: false, isTransparent: true);
                map.AddEntity(goblin);
            }

            posToSpawn = map.WalkabilityView.RandomPosition(true);
            var player = new Player(posToSpawn);
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
            if (mapGenValue) // Floor
                return new BasicTerrain(Color.White, Color.Black, '.', position, isWalkable: true, isTransparent: true);
            else // Wall
                return new BasicTerrain(Color.White, Color.Black, '#', position, isWalkable: false,
                    isTransparent: false);
        }
    }
}