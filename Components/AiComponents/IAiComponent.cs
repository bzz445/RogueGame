using GoRogue.GameFramework.Components;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.Components.AiComponents
{
    public interface IAiComponent: IGameObjectComponent
    {
        bool Run(DungeonMap map, ILogManager logManager);
    }
}