using System;
using Microsoft.Xna.Framework;
using RogueGame.Gui;
using SadConsole;
using Game = SadConsole.Game;

namespace RogueGame
{
    public class RogueGame : IDisposable
    {
        private readonly GuiManager _guiManager;
        private bool _disposedValue;

        public RogueGame()
        {
            _guiManager = new GuiManager();
        }

        public void Run()
        {
            Game.Create(_guiManager.ViewPort.width, _guiManager.ViewPort.height);
            Game.OnInitialize = Init;
            Game.Instance.Run();
        }

        public void Dispose()
        {
            if (!_disposedValue)
            {
                Game.Instance.Dispose();
                _disposedValue = true;
            }

            GC.SuppressFinalize(this);
        }

        private void Init()
        {
            //Global.LoadFont(_guiManager.TileFont.path);
            //Global.FontDefault = Global.Fonts[_guiManager.TileFont.name].GetFont(Font.FontSizes.One);
            _guiManager.TextFont = Global.FontDefault;
            Global.CurrentScreen = _guiManager.Screen;
        }
    }
}