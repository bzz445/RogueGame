using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.GameSystems.Player;
using RogueGame.GameSystems.TurnBasedGame;
using RogueGame.Logging;
using RogueGame.Maps;
using SadConsole;
using SadConsole.Controls;

namespace RogueGame.Ui.Consoles
{
    public class DungeonModeConsole : ContainerConsole
    {
        private const int LeftPaneWidth = 30;
        private const int TopPaneHeight = 3;
        private const int InfoPanelHeight = 8;
        
        private readonly ControlsConsole _leftPane;
        private List<Console> _entitySummaryConsoles;
        
        public DungeonModeConsole(
            int width, 
            int height, 
            Font tileSetFont, 
            IMapModeMenuProvider menuProvider, 
            IMapFactory mapFactory,
            IMapPlan mapPlan,
            ILogManager logManager,
            ITurnBasedGame game,
            Player playerInfo)
        {
            var rightSectionWidth = width - LeftPaneWidth;

            var topPane = new Console(rightSectionWidth, TopPaneHeight);
            topPane.Position = new Point(LeftPaneWidth, 0);

            var tileSizeXFactor = tileSetFont.Size.X / Global.FontDefault.Size.X;
            var map = mapFactory.CreateDungeonMap(80, 45, mapPlan, playerInfo);
            var mapConsole = new DungeonMapConsole(
                rightSectionWidth / tileSizeXFactor,
                height - TopPaneHeight,
                tileSetFont,
                menuProvider,
                game,
                map)
            {
                Position = new Point(LeftPaneWidth, TopPaneHeight)
            };
            mapConsole.SummaryConsolesChanged += (_, args) => HandleNewSummaryConsoles(args.Consoles);
            
            _leftPane = new ControlsConsole(LeftPaneWidth, height)
            {
                ThemeColors = ColorHelper.GetThemeColorsForBackgroundColor(Color.Transparent),
            };
            var infoPanel = new ControlsConsole(LeftPaneWidth, InfoPanelHeight);
            var manaBar = new ProgressBar(30, 1, HorizontalAlignment.Left)
            {
                Position = new Point(0, 4),
            };
            manaBar.ThemeColors = ColorHelper.GetProgressBarThemeColors(ColorHelper.DepletedHealthRed, ColorHelper.ManaBlue);
            manaBar.Progress = 1;

            var healthComponent = mapConsole.Player.GetGoRogueComponent<IHealthComponent>();
            var healthBar = new ProgressBar(30, 1, HorizontalAlignment.Left)
            {
                Position = new Point(0, 3),
            };
            healthBar.ThemeColors = ColorHelper.GetProgressBarThemeColors(ColorHelper.DepletedHealthRed, ColorHelper.HealthRed);
            mapConsole.Player.GetGoRogueComponent<IHealthComponent>().HealthChanged += (_, __) =>
            {
                healthBar.Progress = healthComponent.Health / healthComponent.MaxHealth;
            };
            healthBar.Progress = healthComponent.Health / healthComponent.MaxHealth;

            infoPanel.Add(manaBar);
            infoPanel.Add(healthBar);
            _leftPane.Children.Add(infoPanel);
            
            var eventLog = new MessageLogConsole(LeftPaneWidth, height - InfoPanelHeight, Global.FontDefault)
            {
                Position = new Point(0, InfoPanelHeight),
            };
            logManager.RegisterEventListener(s => eventLog.Add(s));
            logManager.RegisterDebugListener(s => eventLog.Add($"DEBUG: {s}")); // todo put debug logs somewhere else
            
            // test data
            infoPanel.Add(new Label("Vede of Tattersail") { Position = new Point(1, 0), TextColor = Color.Gainsboro });
            infoPanel.Add(new Label("Material Plane, Ayen") { Position = new Point(1, 1), TextColor = Color.DarkGray });
            infoPanel.Add(new Label("Old Alward's Tower") { Position = new Point(1, 2), TextColor = Color.DarkGray });
            
            Children.Add(_leftPane);
            Children.Add(topPane);
            Children.Add(mapConsole);
            Children.Add(eventLog);
            Children.Add(_leftPane);
        }

        private void HandleNewSummaryConsoles(List<Console> consoles)
        {
            _entitySummaryConsoles?.ForEach(c => _leftPane.Children.Remove(c));

            _entitySummaryConsoles = consoles;

            var yOffset = 8;
            _entitySummaryConsoles.ForEach(c =>
            {
                c.Position = new Point(0, yOffset);
                yOffset += c.Height;
                _leftPane.Children.Add(c);
            });
        }
    }
}