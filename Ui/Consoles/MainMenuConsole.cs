﻿using Microsoft.Xna.Framework;
using RogueGame.GameSystems;
using SadConsole;

namespace RogueGame.Ui.Consoles
{
    public sealed class MainMenuConsole : ContainerConsole
    {
        public MainMenuConsole(IGameManager gameManager)
        {
            DefaultBackground = Color.Black;

            var titleFont = Global.FontDefault.Master.GetFont(Font.FontSizes.Three);
            var titleConsole = new Console(160, 45, titleFont);

            titleConsole.Fill(null, Color.Black, null);
            titleConsole.Print(21, 3, "ROGUE GAME");
            titleConsole.DefaultBackground = Color.Black;

            var menuConsole = new ControlsConsole(160, 33)
            {
                Position = new Point(0, 12)
            };

            var continueButton = new SadConsole.Controls.Button(30, 1)
            {
                Text = "Continue",
                Position = new Point(66, 8),
                IsEnabled = false,
            };
            menuConsole.Add(continueButton);

            var newGameButton = new SadConsole.Controls.Button(30, 1)
            {
                Text = "New game",
                Position = new Point(66, 10),
            };
            
            newGameButton.Click += (_, __) => gameManager.Start();
            menuConsole.Add(newGameButton);

            var exitButton = new SadConsole.Controls.Button(30, 1)
            {
                Text = "Exit",
                Position = new Point(66, 12),
            };
            exitButton.Click += (_, __) => SadConsole.Game.Instance.Exit();
            menuConsole.Add(exitButton);

            Children.Add(titleConsole);
            Children.Add(menuConsole);
        }
    }
}