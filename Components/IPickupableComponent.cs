using System.Collections.Generic;
using GoRogue;
using GoRogue.GameFramework;
using RogueGame.GameSystems.Items;

namespace RogueGame.Components
{
    public interface IPickupableComponent: IStepTriggeredComponent
    {
        List<ItemTemplate> Items { get; }
    }
}