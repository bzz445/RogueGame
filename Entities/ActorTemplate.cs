using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RogueGame.Entities
{
    public class ActorTemplate
    {
        public ActorTemplate(
            string id,
            string name,
            int glyph,
            Color nameColor,
            System.Func<List<object>> createComponents)
        {
            Id = id;
            Name = name;
            Glyph = glyph;
            NameColor = nameColor;
            CreateComponents = createComponents;
        }

        public string Id { get; }

        public string Name { get; }

        public int Glyph { get; }

        public Color NameColor { get; }

        public System.Func<List<object>> CreateComponents { get; }
    }
}