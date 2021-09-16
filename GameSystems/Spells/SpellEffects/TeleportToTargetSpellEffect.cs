using GoRogue;
using RogueGame.Entities;
using RogueGame.Logging;
using RogueGame.Maps;

namespace RogueGame.GameSystems.Spells.SpellEffects
{
    public class TeleportToTargetSpellEffect : ISpellEffect
    {
        public void Apply(McEntity caster, DungeonMap map, Coord targetCoord, ILogManager logManager)
        {
            caster.Position = targetCoord;
        }
    }
}