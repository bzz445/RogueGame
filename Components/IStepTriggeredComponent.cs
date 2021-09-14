using GoRogue;
using GoRogue.GameFramework.Components;
using RogueGame.Entities;
using SadConsole;

namespace RogueGame.Components
{
    public interface IStepTriggeredComponent: IGameObjectComponent
    {
        public void OnStep(McEntity steppingEntity);
    }
}