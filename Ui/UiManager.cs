using SadConsole;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Entities;
using RogueGame.GameSystems.Items;
using RogueGame.Logging;
using RogueGame.Maps;
using SadConsole.Controls;
using Console = SadConsole.Console;

namespace RogueGame.Ui
{
    public sealed class UiManager
    {
        private readonly ILogManager _logManager;
        
        public const string TileSetFontPath = "Fonts\\rus.font";
        public const string TileSetFontName = "rus";

        public int ViewPortWidth { get; } = 160; // 160 x 8 = 1280
        public int ViewPortHeight { get; } = 45; // 45 x 16 = 720

        public UiManager()
        {
            _logManager = new LogManager();
        }
        
        public Console CreateMainMenu()
        {
            return new MainMenu(this);
        }

        private IMenuProvider CreateMenuProvider()
        {
            var inventory = new InventoryWindow(120, 30);

            return new MenuProvider(inventory);
        }

        public ContainerConsole CreateMapScreen()
        {
            var tileSetFont = Global.Fonts[TileSetFontName].GetFont(Font.FontSizes.One);
            var entityFactory = new EntityFactory(tileSetFont, _logManager);
            return new MapScreen(
                ViewPortWidth, 
                ViewPortHeight, 
                tileSetFont,
                CreateMenuProvider(),
                entityFactory,
                _logManager);
        }
    }
}