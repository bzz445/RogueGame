﻿using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Themes;

namespace RogueGame.Ui
{
    public sealed class MainMenu : ContainerConsole
    {
        public MainMenu(Font font, UiManager uiManager)
        {
            DefaultBackground = Color.Black;

            var titleFont = font.Master.GetFont(Font.FontSizes.Three);
            var titleConsole = new Console(160, 45, titleFont);

            titleConsole.Fill(null, Color.Black, null);
            titleConsole.Print(21, 3, "ROGUE GAME");
            titleConsole.DefaultBackground = Color.Black;

            var menuConsole = new ControlsConsole(160, 33, font);
            menuConsole.Position = new Point(0, 12);

            var continueButton = new SadConsole.Controls.Button(12, 1)
            {
                Text = "Continue",
                Position = new Point(76, 8),
                IsEnabled = false,
            };
            menuConsole.Add(continueButton);

            var newGameButton = new SadConsole.Controls.Button(12, 1)
            {
                Text = "New Game",
                Position = new Point(76, 10),
            };
            newGameButton.Click += (_, __) => Global.CurrentScreen = uiManager.CreateMapScreen();
            menuConsole.Add(newGameButton);

            var exitButton = new SadConsole.Controls.Button(12, 1)
            {
                Text = "Exit",
                Position = new Point(76, 12),
            };
            exitButton.Click += (_, __) => SadConsole.Game.Instance.Exit();
            menuConsole.Add(exitButton);

            Children.Add(titleConsole);
            Children.Add(menuConsole);
        }
    }
}
