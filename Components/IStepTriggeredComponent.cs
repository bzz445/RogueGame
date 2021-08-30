using GoRogue;
using GoRogue.GameFramework.Components;

namespace RogueGame.Components
{
    public interface IStepTriggeredComponent: IGameObjectComponent
    {
        public void OnStep(IHasComponents steppingEntry);
    }
}