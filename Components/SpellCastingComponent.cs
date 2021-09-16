using System.Collections.Generic;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using RogueGame.GameSystems.Spells;

namespace RogueGame.Components
{
    public class SpellCastingComponent : IGameObjectComponent, ISpellCastingComponent
    {
        public SpellCastingComponent(params SpellTemplate[] spells)
        {
            Spells = new List<SpellTemplate>(spells);
        }

        public IGameObject Parent { get; set; }

        public List<SpellTemplate> Spells { get; }
    }
}