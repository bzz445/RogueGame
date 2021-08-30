using System.Collections.Generic;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.GameSystems.Items;

namespace RogueGame.Components
{
    public class PickupableComponent: IGameObjectComponent, IPickupableComponent
    {
        public PickupableComponent(params IInventoryItem[] items)
        {
            Items = new List<IInventoryItem>(items);
        }
        
        public List<IInventoryItem> Items { get; }
        
        public IGameObject Parent { get; set; }
        
        public void OnStep(IHasComponents steppingEntry)
        {
            var inventory = steppingEntry.GetComponent<IInventoryComponent>();
            if (inventory == null)
            {
                return;
            }
            
            inventory.Items.AddRange(Items);
            Parent.CurrentMap.RemoveEntity(Parent);
        }
    }
}