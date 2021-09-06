using RogueGame.Components;
using RogueGame.Ui.Windows;

namespace RogueGame.Ui
{
    public interface IMapModeMenuProvider
    {
        InventoryWindow Inventory { get; }
        
        DeathWindow Death { get; }
    }
}