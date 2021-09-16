using GoRogue;
using RogueGame.Entities;
using RogueGame.GameSystems.Spells;
using RogueGame.Maps;

namespace RogueGame.GameSystems.TurnBasedGame
{
    public interface ITurnBasedGame
    {
        DungeonMap Map { get; set; }
        State State { get; set; }
        SpellTemplate TargettingSpell { get; }
        
        bool HandleAsPlayerInput(SadConsole.Input.Keyboard info);
        void RegisterEntity(McEntity entity);
        void UnregisterEntity(McEntity entity);
        void RegisterPlayer(Wizard wizard);
        void TargetSelected(Coord mapCoord);
        void StartTargetting(SpellTemplate spell);
    }
}