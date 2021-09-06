using System.Collections.Generic;
using RogueGame.GameSystems.Items;

namespace RogueGame.Maps
{
    public class MapPlan: IMapPlan
    {
        public MapPlan()
        {
            FloorItems = new List<ItemTemplate>();
        }
        public List<ItemTemplate> FloorItems { get; }
    }
}