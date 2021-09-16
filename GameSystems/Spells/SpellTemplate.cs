using System.Collections.Generic;

namespace RogueGame.GameSystems.Spells
{
    public class SpellTemplate
    {
        public SpellTemplate(
            string id,
            string name,
            string description,
            int iconGlyph,
            ITargettingStyle targettingStyle,
            List<ISpellEffect> effects)
        {
            Id = id;
            Name = name;
            Description = description;
            IconGlyph = iconGlyph;
            TargettingStyle = targettingStyle;
            Effects = effects;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int IconGlyph { get; }

        public ITargettingStyle TargettingStyle { get; }
        
        public List<ISpellEffect> Effects { get; }
    }
}