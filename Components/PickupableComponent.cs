using System.Collections.Generic;
using System.Linq;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.Entities;
using RogueGame.GameSystems.Items;
using RogueGame.Logging;
using SadConsole;

namespace RogueGame.Components
{
    public class PickupableComponent: IGameObjectComponent, IPickupableComponent
    {
        private readonly ILogManager _logManager;
        
        public PickupableComponent(ILogManager logManager, params ItemTemplate[] items)
        {
            Items = new List<ItemTemplate>(items);
            _logManager = logManager;
        }
        
        public List<ItemTemplate> Items { get; }
        
        public IGameObject Parent { get; set; }
        
        public void OnStep(McEntity steppingEntity)
        {
            var inventory = steppingEntity.GetGoRogueComponent<IInventoryComponent>();
            if (inventory == null)
            {
                return;
            }
            
            inventory.Items.AddRange(Items);
            Parent.CurrentMap.RemoveEntity(Parent);
            
            _logManager.EventLog($"{steppingEntity.ColoredName} picked up {string.Join(", ", Items.Select(i => i.Name))}.");
        }
    }
}