﻿using System.Collections.Generic;
using System.Linq;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueGame.Components;
using RogueGame.Entities;
using RogueGame.GameSystems.Spells;
using RogueGame.GameSystems.TurnBasedGame;
using RogueGame.Maps;
using SadConsole;
using SadConsole.Input;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace RogueGame.Ui.Consoles
{
   internal class DungeonMapConsole : ContainerConsole
    {
        private readonly IMapModeMenuProvider _menuProvider;
        private readonly Console _mouseHighlight;
        private readonly ITurnBasedGame _game;

        private Point _lastSummaryConsolePosition;

        public event System.EventHandler<ConsoleListEventArgs> SummaryConsolesChanged;
        public event System.EventHandler<string> FlavorMessageChanged;

        public DungeonMap Map { get; }

        public ScrollingConsole MapRenderer { get; }

        public Wizard Player { get; }

        public DungeonMapConsole(
            int viewportWidth,
            int viewportHeight,
            Font tilesetFont,
            IMapModeMenuProvider menuProvider,
            ITurnBasedGame game,
            DungeonMap map)
        {
            _menuProvider = menuProvider;
            _game = game;

            _mouseHighlight = new Console(viewportWidth, viewportHeight, tilesetFont)
            {
                UseMouse = false,
            };

            Map = map;
            _game.Map = map;

            foreach (var entity in map.Entities.Items.OfType<McEntity>())
            {
                if (entity is Wizard player)
                {
                    Player = player;
                    _game.RegisterPlayer(player);
                    player.RemovedFromMap += Player_RemovedFromMap;
                    Player.Moved += Player_Moved;
                    continue;
                }

                _game.RegisterEntity(entity);
            }

            MapRenderer = Map.CreateRenderer(new XnaRect(0, 0, viewportWidth, viewportHeight), tilesetFont);
            MapRenderer.UseMouse = false;
            IsFocused = true;

            Map.CalculateFOV(Player.Position, Player.FOVRadius, Radius.SQUARE);
            MapRenderer.CenterViewPortOnPoint(Player.Position);

            Children.Add(MapRenderer);
            Children.Add(_mouseHighlight);
        }

        private void Player_RemovedFromMap(object sender, System.EventArgs e)
        {
            _menuProvider.Death.Show("You died.");
        }

        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (!Player.HasMap)
            {
                return base.ProcessKeyboard(info);
            }

            switch (_game.State)
            {
                case State.PlayerTurn:
                    return PlayerTurnProcessKeyboard(info);
                case State.Processing:
                    return base.ProcessKeyboard(info);
                case State.Targetting:
                    return TargettingProcessKeyboard(info);
                default:
                    return base.ProcessKeyboard(info);
            }
        }

        public override bool ProcessMouse(MouseConsoleState state)
        {
            if (!Player.HasMap)
            {
                return base.ProcessMouse(state);
            }

            var mapState = new MouseConsoleState(MapRenderer, state.Mouse);

            var mapCoord = new Coord(
                mapState.ConsoleCellPosition.X + MapRenderer.ViewPort.X,
                mapState.ConsoleCellPosition.Y + MapRenderer.ViewPort.Y);

            DrawMouseHighlight(mapState, mapCoord);

            var coordIsTargetable = mapState.IsOnConsole && Map.FOV.CurrentFOV.Contains(mapCoord);

            if (coordIsTargetable && _lastSummaryConsolePosition != mapState.ConsoleCellPosition)
            {
                // update summaries
                var summaryControls = new List<Console>();
                foreach (var entity in Map.GetEntities<BasicEntity>(mapCoord))
                {
                    var control = entity.GetGoRogueComponent<ISummaryControlComponent>()?.GetSidebarSummary();
                    if (control != null)
                    {
                        summaryControls.Add(control);
                    }
                }

                _lastSummaryConsolePosition = mapState.ConsoleCellPosition;
                SummaryConsolesChanged?.Invoke(this, new ConsoleListEventArgs(summaryControls));
            }
            
            if (!_mouseHighlight.IsVisible && _lastSummaryConsolePosition != default)
            {
                // remove the summaries if we just moved out of a valid location
                _lastSummaryConsolePosition = default;
                SummaryConsolesChanged?.Invoke(this, new ConsoleListEventArgs(new List<Console>()));
            }

            if (coordIsTargetable && _game.State == State.Targetting)
            {
                TargettingProcessMouse(state, mapCoord);
            }

            return base.ProcessMouse(state);
        }

        private void DrawMouseHighlight(MouseConsoleState state, Coord mapCoord)
        {
            _mouseHighlight.IsVisible = state.IsOnConsole && Map.Explored[mapCoord];
            if (!_mouseHighlight.IsVisible)
            {
                return;
            }

            _mouseHighlight.Clear();
            var mousePos = state.ConsoleCellPosition;
            if (_game.State == State.PlayerTurn)
            {
                _mouseHighlight.SetGlyph(mousePos.X, mousePos.Y, 1, ColorHelper.WhiteHighlight);
                return;
            }

            if (_game.State != State.Targetting)
            {
                return;
            }

            var highlightColor = _game.TargettingSpell.TargettingStyle.Offensive
                ? ColorHelper.RedHighlight
                : ColorHelper.YellowHighlight;
            _mouseHighlight.SetGlyph(mousePos.X, mousePos.Y, 1, highlightColor);
        }

        private void TargettingProcessMouse(MouseConsoleState state, Coord mapCoord)
        {
            if (state.Mouse.LeftClicked)
            {
                _game.TargetSelected(mapCoord);
                EndTargettingMode();
            }
        }

        private bool PlayerTurnProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyPressed(Keys.Escape))
            {
                _menuProvider.Pop.Show();
                return true;
            }

            if (info.IsKeyPressed(Keys.I))
            {
                _menuProvider.Inventory.Show(Player.GetGoRogueComponent<IInventoryComponent>());
                return true;
            }

            if (info.IsKeyPressed(Keys.Q))
            {
                _menuProvider.SpellSelect.Show(
                    Player.GetGoRogueComponent<ISpellCastingComponent>().Spells,
                    selectedSpell =>
                        {
                            BeginTargetting(selectedSpell);
                        });

                return true;
            }

            if (_game.HandleAsPlayerInput(info))
            {
                _lastSummaryConsolePosition = default;
                return true;
            }

            return base.ProcessKeyboard(info);
        }

        private bool TargettingProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyPressed(Keys.Escape))
            {
                EndTargettingMode();
                _menuProvider.Pop.Show();
                return true;
            }

            if (info.IsKeyPressed(Keys.I))
            {
                EndTargettingMode();
                _menuProvider.Inventory.Show(Player.GetGoRogueComponent<IInventoryComponent>());
                return true;
            }

            // handle enter as confirm target

            return base.ProcessKeyboard(info);
        }

        private void BeginTargetting(SpellTemplate spell)
        {
            _game.StartTargetting(spell);
            FlavorMessageChanged?.Invoke(this, $"Aiming {spell.Name}...");
        }

        private void EndTargettingMode()
        {
            _game.State = State.PlayerTurn;
            FlavorMessageChanged?.Invoke(this, string.Empty);
            _mouseHighlight.SetGlyph(0, 0, 1, ColorHelper.WhiteHighlight);
        }

        private void Player_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            MapRenderer.CenterViewPortOnPoint(Player.Position);
        }
    }
}