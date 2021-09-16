using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueGame.GameSystems;
using SadConsole;
using SadConsole.Controls;
using Keyboard = SadConsole.Input.Keyboard;

namespace RogueGame.Ui.Windows
{
    public class PopupMenuWindow: Window
    {
        private bool _escReleased;

        public PopupMenuWindow(UiManager uiManager, IGameManager gameManager) 
            : base(30, 7)
        {
            CloseOnEscKey = false; // it would close as soon as it opens...
            IsModalDefault = true;
            Center();

            var background = new Console(Width, Height);
            background.Fill(null, ColorHelper.GreyHighlight, null);
            
            Children.Add(background);

            const string mainMenuText = "Exit to Main Menu";
            var mainMenuButtonWidth = mainMenuText.Length + 4;
            var mainMenuButton = new SelectionButton(mainMenuButtonWidth, 1)
            {
                Text = mainMenuText,
                Position = new Point((Width / 2) - (mainMenuButtonWidth / 2), Height - 6),
            };
            mainMenuButton.Click += (_, __) =>
            {
                Hide();
                uiManager.ShowMainMenu(gameManager);
            };
            
            const string quitText = "Exit to Desktop";
            var quitButtonWidth = mainMenuText.Length + 4;
            var quitButton = new SelectionButton(quitButtonWidth, 1)
            {
                Text = quitText,
                Position = new Point((Width / 2) - (quitButtonWidth / 2), Height - 4),
            };
            quitButton.Click += (_, __) => System.Environment.Exit(0);

            const string closeText = "Close";
            var closeButtonWidth = closeText.Length + 4;
            var closeButton = new SelectionButton(closeButtonWidth, 1)
            {
                Text = closeText,
                Position = new Point((Width / 2) - (closeButtonWidth / 2), Height - 2),
            };
            closeButton.Click += (_, __) => Hide();
            
            mainMenuButton.NextSelection = quitButton;
            mainMenuButton.PreviousSelection = closeButton;
            quitButton.NextSelection = closeButton;
            quitButton.PreviousSelection = mainMenuButton;
            closeButton.NextSelection = mainMenuButton;
            closeButton.PreviousSelection = quitButton;
            
            Add(mainMenuButton);
            Add(quitButton);
            Add(closeButton);
            
            FocusedControl = mainMenuButton;
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            if (!info.IsKeyPressed(Keys.Escape))
            {
                _escReleased = true;
                return base.ProcessKeyboard(info);
            }

            if (_escReleased)
            {
                Hide();
                return true;
            }
            return base.ProcessKeyboard(info);
        }
    }
}