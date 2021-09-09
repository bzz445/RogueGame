using SadConsole;
using RogueGame.Entities;
using RogueGame.GameSystems;
using RogueGame.Logging;
using RogueGame.Maps;
using RogueGame.Ui.Consoles;
using RogueGame.Ui.Windows;

namespace RogueGame.Ui
{
    public sealed class UiManager: IUiManager
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

        public void ShowMainMenu(IGameManager gameManager)
        {
            var menu = new MainMenuConsole(gameManager);
            Global.CurrentScreen = menu;
            menu.IsFocused = true;
        }

        public ContainerConsole CreateDungeonMapScreen(IMapPlan mapPlan, IGameManager gameManager)
        {
            var tilesetFont = Global.Fonts[TileSetFontName].GetFont(Font.FontSizes.One);
            var entityFactory = new EntityFactory(tilesetFont, _logManager);
            var mapFactory = new MapFactory(entityFactory);
            return new DungeonModeConsole(
                ViewPortWidth,
                ViewPortHeight,
                tilesetFont,
                CreateMenuProvider(gameManager),
                mapFactory,
                mapPlan,
                _logManager);
        }

        public CastleModeConsole CreateCastleMapScreen(IMapPlan mapPlan, IGameManager gameManager)
        {
            var tilesetFont = Global.Fonts[TileSetFontName].GetFont(Font.FontSizes.One);
            var entityFactory = new EntityFactory(tilesetFont, _logManager);
            var mapFactory = new MapFactory(entityFactory);
            return new CastleModeConsole(
                ViewPortWidth,
                ViewPortHeight,
                tilesetFont,
                CreateMenuProvider(gameManager),
                mapFactory,
                mapPlan,
                _logManager);
        }

        private IMapModeMenuProvider CreateMenuProvider(IGameManager gameManager)
        {
            var inventory = new InventoryWindow(120, 30);
            var death = new DeathWindow(this, gameManager);

            return new MapModeMenuProvider(inventory, death);
        }
    }
}