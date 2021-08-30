using System.Collections.Generic;
using RogueGame.GameSystems.Items;

namespace RogueGame.Components
{
    public interface IInventoryComponent
    {
        List<IInventoryItem> Items { get; }
    }
}