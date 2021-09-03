using GoRogue;
using GoRogue.GameFramework.Components;
using SadConsole;

namespace RogueGame.Components
{
    public interface IStepTriggeredComponent: IGameObjectComponent
    {
        public void OnStep(BasicEntity steppingEntity);
    }
}