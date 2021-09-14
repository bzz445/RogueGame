using System.Collections.Generic;
using RogueGame.GameSystems.Items;

namespace RogueGame.GameSystems.Player
{
    public class Player
    {
        public string Name { get; set; }

        public float Health { get; set; }

        public float MaxHealth { get; set; }

        public List<ItemTemplate> Items { get; set; }

        public static Player CreateDefault() => new Player()
        {
            Name = "Vede",
            Health = 100,
            MaxHealth = 100,
            Items = new List<ItemTemplate>(),
        };
    }
}