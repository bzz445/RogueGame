using System;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using RogueGame.Maps;
using SadConsole;
using Console = SadConsole.Console;

namespace RogueGame.Gui
{
    public sealed class GuiManager
    {
        public (string name, string path) TileFont { get; } = (name: "Terminal8x8", path: "Fonts\\Terminal8x8.font");
        public Font TextFont { get; set; }
        public (int width, int height) ViewPort { get; } = (width: 80, height: 25);
        public ContainerConsole Screen => _screen.Value;
        public MapScreen MapConsole { get; private set; }
        public MessageLog EventLog { get; private set; }

        private readonly Lazy<ContainerConsole> _screen;

        public GuiManager()
        {
            _screen = new Lazy<ContainerConsole>(InitScreen);
        }

        private ContainerConsole InitScreen()
        {
            MapConsole = new MapScreen(80, 25, ViewPort.width, (int)Math.Round(ViewPort.height * 0.9));

            EventLog = new MessageLog(ViewPort.width, ViewPort.height - MapConsole.Height, TextFont)
            {
                Position = new Point(0, MapConsole.MapRenderer.ViewPort.Height)
            };
            
            EventLog.Add("test message 1");
            EventLog.Add("test message 2");
            EventLog.Add("test message 3");
            EventLog.Add("test message 4");
            var screen = new ContainerConsole();
            screen.Children.Add(MapConsole);
            screen.Children.Add(EventLog);
            return screen;
        }
    }
}