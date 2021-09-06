using RogueGame.Ui.Windows;

namespace RogueGame.Ui
{
    public class MenuProvider : IMapModeMenuProvider
    {
        public MenuProvider(InventoryWindow inventory,
            DeathWindow death)
        {
            Inventory = inventory;
            Death = death;
        }

        public InventoryWindow Inventory { get; }
        
        public DeathWindow Death { get; }
    }
}