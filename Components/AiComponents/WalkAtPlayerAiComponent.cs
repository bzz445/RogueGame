using System.Linq;
using GoRogue;
using GoRogue.GameFramework;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.Components.AiComponents
{
    public class WalkAtPlayerAiComponent : IAiComponent
    {
        private readonly int _range;

        public WalkAtPlayerAiComponent(int range)
        {
            _range = range;
        }

        public IGameObject Parent { get; set; }

        public bool Run(DungeonMap map, ILogManager logManager)
        {
            if (!(Parent is McEntity mcParent))
            {
                return false;
            }

            var walkSpeed = mcParent.GetGoRogueComponent<IActorStatComponent>()?.WalkSpeed ?? 1;

            // if we bump into something, stop moving.
            // walk speed doesn't allow you to attack or interact more than once.
            var bumped = false;
            mcParent.Bumped += (_, __) => bumped = true;
            for ( int i = 0; i < walkSpeed; i++)
            {
                GetDirectionAndMove(map, mcParent);
                if (bumped)
                {
                    break;
                }
            }
            
            return true;
        }

        public void GetDirectionAndMove(DungeonMap map, McEntity mcParent)
        {
            var path = map.AStar.ShortestPath(Parent.Position, map.Player.Position);

            Direction direction;
            if (path == null || path.Length > _range)
            {
                // can't reach player or player is far away, move randomly
                var directionType = SadConsole.Global.Random.Next(0, 8);
                direction = Direction.ToDirection((Direction.Types)directionType);
            }
            else
            {
                direction = Direction.GetDirection(path.Steps.First() - Parent.Position);
            }

            mcParent.Move(direction);
        }
    }
}