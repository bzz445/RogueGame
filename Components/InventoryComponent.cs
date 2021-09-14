using System.Collections.Generic;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.GameSystems.Items;

namespace RogueGame.Components
{
    public class InventoryComponent : IGameObjectComponent, IInventoryComponent
    {
        public InventoryComponent(params ItemTemplate[] items)
        {
            Items = new List<ItemTemplate>(items);
        }

        public IGameObject Parent { get; set; }
        public List<ItemTemplate> Items { get; }
    }
}