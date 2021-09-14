using System.Collections.Generic;
using System.Linq;
using GoRogue;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework.Input;
using RogueGame.Components;
using RogueGame.Components.AiComponents;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame.GameSystems.TurnBasedGame
{
    public enum State
    {
        PlayerTurn,
        Processing,
        Targetting
    }
    
    public class TurnBasedGame: ITurnBasedGame
    {
        private static readonly Dictionary<Keys, Direction> MovementDirectionMapping = new Dictionary<Keys, Direction>
        {
            { Keys.NumPad7, Direction.UP_LEFT },
            { Keys.NumPad8, Direction.UP },
            { Keys.NumPad9, Direction.UP_RIGHT },
            { Keys.NumPad4, Direction.LEFT },
            { Keys.NumPad6, Direction.RIGHT },
            { Keys.NumPad1, Direction.DOWN_LEFT },
            { Keys.NumPad2, Direction.DOWN },
            { Keys.NumPad3, Direction.DOWN_RIGHT },
            { Keys.Up, Direction.UP },
            { Keys.Down, Direction.DOWN },
            { Keys.Left, Direction.LEFT },
            { Keys.Right, Direction.RIGHT }
        };

        private readonly ILogManager _logManager;

        private Wizard _player;
        
        public TurnBasedGame(
            ILogManager logManager)
        {
            _logManager = logManager;
            State = State.PlayerTurn;
        }

        public DungeonMap Map { get; set; }

        public State State { get; set; }
        
        public bool HandleAsPlayerInput(SadConsole.Input.Keyboard info)
        {
            foreach (Keys key in MovementDirectionMapping.Keys)
            {
                if (info.IsKeyPressed(key))
                {
                    _player.Move(MovementDirectionMapping[key]);

                    ProcessTurn();
                    return true;
                }
            }

            return false;
        }

        public void RegisterPlayer(Wizard player)
        {
            _player = player;
            RegisterEntity(_player);
        }

        public void RegisterEntity(McEntity entity)
        {
            entity.Moved += Entity_Moved;
            entity.Bumped += Entity_Bumped;
            entity.RemovedFromMap += (_, __) => UnregisterEntity(entity);
        }

        public void UnregisterEntity(McEntity entity)
        {
            entity.Moved -= Entity_Moved;
            entity.Bumped -= Entity_Bumped;
        }

        private void ProcessTurn()
        {
            State = State.Processing;
            foreach (var entity in Map.Entities.Items.OfType<McEntity>().ToList())
            {
                if (!_player.HasMap)
                {
                    break;
                }
                
                if (entity.CurrentMap != Map)
                {
                    continue;
                }

                var ai = entity.GetGoRogueComponent<IAiComponent>();
                ai?.Run(Map, _logManager);
            }
            State = State.PlayerTurn;
        }
        
        private void Entity_Bumped(object sender, ItemMovedEventArgs<McEntity> e)
        {
            //_logManager.DebugLog($"{e.Item.Name} bumped into something.");
            var meleeAttackComponent = e.Item.GetGoRogueComponent<IMeleeAttackerComponent>();
            if (meleeAttackComponent != null)
            {
                var healthComponent = Map.GetEntities<BasicEntity>(e.NewPosition)
                    .SelectMany(e =>
                    {
                        if (!(e is IHasComponents entity))
                        {
                            return new IHealthComponent[0];
                        }

                        return entity.GetComponents<IHealthComponent>();
                    })
                    .FirstOrDefault();
                if (healthComponent != null)
                {
                    var damage = meleeAttackComponent.GetDamage();
                    healthComponent.ApplyDamage(damage, _logManager);

                    var targetName = (healthComponent.Parent as McEntity)?.ColoredName ?? "something";
                    _logManager.EventLog($"{e.Item.ColoredName} hit {targetName} for {damage:F0} damage.");
                }
            }
        }

        private void Entity_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            if (!(e.Item is McEntity movingEntity))
            {
                return;
            }

            if (movingEntity == _player)
            {
                Map.CalculateFOV(_player.Position, _player.FOVRadius, Radius.SQUARE);
            }

            var stepTriggers = Map.GetEntities<McEntity>(movingEntity.Position)
                .SelectMany(e =>
                {
                    if (!(e is IHasComponents entity))
                    {
                        return new IStepTriggeredComponent[0];
                    }

                    return entity.GetComponents<IStepTriggeredComponent>();
                });

            foreach (var trigger in stepTriggers)
            {
                trigger.OnStep(movingEntity);
            }
        }
    }
}