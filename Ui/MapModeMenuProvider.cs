using RogueGame.Ui.Windows;

namespace RogueGame.Ui
{
    public class MapModeMenuProvider : IMapModeMenuProvider
    {
        public MapModeMenuProvider(InventoryWindow inventory,
            DeathWindow death)
        {
            Inventory = inventory;
            Death = death;
        }

        public InventoryWindow Inventory { get; }
        
        public DeathWindow Death { get; }
    }
}