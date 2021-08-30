using System.Collections.Generic;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.GameSystems.Items;

namespace RogueGame.Components
{
    public class InventoryComponent : IGameObjectComponent, IInventoryComponent
    {
        public InventoryComponent()
        {
            Items = new List<IInventoryItem>();
        }

        public IGameObject Parent { get; set; }
        public List<IInventoryItem> Items { get; }
    }
}