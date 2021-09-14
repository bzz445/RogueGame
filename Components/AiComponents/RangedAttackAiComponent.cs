using GoRogue.GameFramework;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.Components.AiComponents
{
    public class RangedAttackAiComponent : IAiComponent
    {
        public IGameObject Parent { get; set; }

        public bool Run(DungeonMap map, ILogManager logManager)
        {
            if (!(Parent is McEntity mcParent))
            {
                return false;
            }

            var rangedAttackComponent = mcParent.GetGoRogueComponent<IRangedAttackerComponent>();
            return rangedAttackComponent.TryAttack(map, logManager);
        }
    }
}