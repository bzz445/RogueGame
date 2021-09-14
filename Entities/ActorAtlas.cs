using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.Components.AiComponents;
using RogueGame.Fonts;

namespace RogueGame.Entities
{
    public static class ActorAtlas
    {
        static ActorAtlas()
        {
            ActorsById = new Dictionary<string, ActorTemplate>
            {
                { Goblin.Id, Goblin },
                { GoblinArcher.Id, GoblinArcher },
                { Warg.Id, Warg },
            };
        }

        public static ActorTemplate Goblin => new ActorTemplate()
        {
            Id = "ACTOR_GOBLIN",
            Name = "Goblin",
            Glyph = SpriteAtlas.Goblin,
            NameColor = Color.DarkGreen,
            CreateComponents = () => new List<object>
            {
                new HealthComponent(10),
                new ActorStatComponent(1),
                new MeleeAttackerComponent(5),
                new WalkAtPlayerAiComponent(6),
            },
        };

        public static ActorTemplate GoblinArcher => new ActorTemplate()
        {
            Id = "ACTOR_GOBLIN_ARCHER",
            Name = "Goblin archer",
            Glyph = SpriteAtlas.GoblinArcher,
            NameColor = Color.DarkGreen,
            CreateComponents = () => new List<object>
            {
                new HealthComponent(10),
                new ActorStatComponent(1),
                new RangedAttackerComponent(5, 4),
                new LinearCompositeAiComponent(
                    new RangedAttackAiComponent(),
                    new WalkAtPlayerAiComponent(6)),
            },
        };

        public static ActorTemplate Warg => new ActorTemplate()
        {
            Id = "ACTOR_WARG",
            Name = "Warg",
            Glyph = SpriteAtlas.Warg,
            NameColor = Color.DarkSlateGray,
            CreateComponents = () => new List<object>
            {
                new HealthComponent(10),
                new ActorStatComponent(2),
                new MeleeAttackerComponent(5),
                new WalkAtPlayerAiComponent(6),
            },
        };

        public static Dictionary<string, ActorTemplate>  ActorsById { get; }
    }
}