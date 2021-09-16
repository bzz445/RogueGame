using GoRogue;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.GameSystems.Spells
{
    public interface ISpellEffect
    {
        void Apply(McEntity caster, DungeonMap map, Coord targetCoord, ILogManager logManager);
    }
}