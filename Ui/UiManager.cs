using SadConsole;
using System;
using Microsoft.Xna.Framework;
using RogueGame.Maps;
using Console = SadConsole.Console;

namespace RogueGame.Ui
{
    public sealed class UiManager
    {
        public const string TileSetFontPath = "Fonts\\rus.font";
        public const string TileSetFontName = "rus";

        public static Color MidnightEstBlue = new Color(3, 3, 15);
        
        public int ViewPortWidth { get; } = 160; // 160 x 8 = 1280
        public int ViewPortHeight { get; } = 45; // 45 x 16 = 720

        public Console CreateMainMenu()
        {
            return new MainMenu(this);
        }

        public ContainerConsole CreateMapScreen()
        {
            const int leftPaneWidth = 30;
            const int topPaneHeight = 3;
            const int eventLogHeight = 4;

            var leftPane = new Console(leftPaneWidth, ViewPortHeight);
            leftPane.Fill(null, MidnightEstBlue, null);

            var rightSectionWidth = ViewPortWidth - leftPaneWidth;

            var topPane = new Console(rightSectionWidth, topPaneHeight);
            topPane.Position = new Point(leftPaneWidth, 0);
            topPane.Fill(null, MidnightEstBlue, null);
            
            var titleSetFont = Global.Fonts[TileSetFontName].GetFont(Font.FontSizes.One);
            var tileSizeXFactor = titleSetFont.Size.X / Global.FontDefault.Size.X;
            var mapConsole = new MapScreen(80, 45, rightSectionWidth/tileSizeXFactor, ViewPortHeight - eventLogHeight - topPaneHeight, titleSetFont);
            mapConsole.Position = new Point(leftPaneWidth, topPaneHeight);
            
            var eventLog = new MessageLog(ViewPortWidth, eventLogHeight, Global.FontDefault);
            eventLog.Position = new Point(leftPaneWidth, mapConsole.MapRenderer.ViewPort.Height + topPaneHeight);
            eventLog.Add("Hello world!");

            var screen = new ContainerConsole();
            screen.Children.Add(leftPane);
            screen.Children.Add(topPane);
            screen.Children.Add(mapConsole);
            screen.Children.Add(eventLog);

            return screen;
        }
    }
}