using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.Components.AiComponents
{
    public interface IRangedAttackerComponent
    {
        bool TryAttack(DungeonMap map, ILogManager logManager);
    }
}