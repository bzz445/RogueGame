using GoRogue.GameFramework.Components;
using RogueGame.Maps;

namespace RogueGame.Components.AiComponents
{
    public interface IAiComponent: IGameObjectComponent
    {
        void Run(DungeonMap map);
    }
}