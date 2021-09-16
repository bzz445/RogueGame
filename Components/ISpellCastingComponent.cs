using System.Collections.Generic;
using RogueGame.GameSystems.Spells;

namespace RogueGame.Components
{
    public interface ISpellCastingComponent
    {
        List<SpellTemplate> Spells { get; }
    }
}