using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Entities;
using RogueGame.GameSystems.Items;
using RogueGame.Logging;
using RogueGame.Maps;
using SadConsole;
using SadConsole.Controls;

namespace RogueGame.Ui
{
    public class MapScreen : ContainerConsole
    {
        private const int leftPaneWidth = 30;
        private const int topPaneHeight = 3;
        private const int eventLogHeight = 4;

        private readonly ControlsConsole _leftPane;
        private List<Console> _entitySummaryConsoles;
        
        public MapScreen(
            int width, 
            int height, 
            Font tileSetFont, 
            IMenuProvider menuProvider, 
            IEntityFactory entityFactory,
            ILogManager logManager)
        {
            var rightSectionWidth = width - leftPaneWidth;

            var topPane = new Console(rightSectionWidth, topPaneHeight);
            topPane.Position = new Point(leftPaneWidth, 0);

            var tileSizeXFactor = tileSetFont.Size.X / Global.FontDefault.Size.X;
            var mapConsole = new MapConsole(
                80,
                45,
                rightSectionWidth / tileSizeXFactor,
                height - eventLogHeight - topPaneHeight,
                tileSetFont,
                menuProvider,
                entityFactory)
            {
                Position = new Point(leftPaneWidth, topPaneHeight)
            };
            mapConsole.SummaryConsolesChanged += (_, args) => HandleNewSummaryConsoles(args.Consoles);
            
            _leftPane = new ControlsConsole(leftPaneWidth, height);
            var manaBar = new ProgressBar(30, 1, HorizontalAlignment.Left)
            {
                Position = new Point(0, 4),
            };
            manaBar.ThemeColors = ColorHelper.GetProgressBarThemeColors(Color.White, ColorHelper.ManaBlue);
            manaBar.Progress = 1;

            var healthComponent = mapConsole.Player.GetGoRogueComponent<IHealthComponent>();
            var healthBar = new ProgressBar(30, 1, HorizontalAlignment.Left)
            {
                Position = new Point(0, 3),
            };
            healthBar.ThemeColors = ColorHelper.GetProgressBarThemeColors(Color.White, Color.DarkRed);
            healthBar.Progress = healthComponent.Health / healthComponent.MaxHealth;

            _leftPane.Add(manaBar);
            _leftPane.Add(healthBar);
            
            var eventLog = new MessageLog(width, eventLogHeight, Global.FontDefault);
            eventLog.Position = new Point(leftPaneWidth, mapConsole.MapRenderer.ViewPort.Height + topPaneHeight);
            logManager.RegisterEventListener(s => eventLog.Add(s));
            
            // test data...
            mapConsole.Player.GetGoRogueComponent<IInventoryComponent>().Items.Add(new InventoryItem(
                "trusty oak staff",
                "Cut from the woods of the Academy at Kurisau, this staff has served you since you first learned to sense the Wellspring."));
            eventLog.Add("Find the tower's core.");
            _leftPane.Add(new Label("Vede of Tattersail") { Position = new Point(1, 0), TextColor = Color.Gainsboro });
            _leftPane.Add(new Label("Material Plane, Ayen") { Position = new Point(1, 1), TextColor = Color.DarkGray });
            _leftPane.Add(new Label("Old Alward's Tower") { Position = new Point(1, 2), TextColor = Color.DarkGray });
            
            Children.Add(_leftPane);
            Children.Add(topPane);
            Children.Add(mapConsole);
            Children.Add(eventLog);
        }

        private void HandleNewSummaryConsoles(List<Console> consoles)
        {
            _entitySummaryConsoles?.ForEach(c => _leftPane.Children.Remove(c));

            _entitySummaryConsoles = consoles;

            var yOffset = 8;
            _entitySummaryConsoles.ForEach(c =>
            {
                c.Position = new Point(0, yOffset);
                yOffset += c.Height + 1;
                _leftPane.Children.Add(c);
            });
        }
    }
}