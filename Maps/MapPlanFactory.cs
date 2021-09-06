using System.Collections;
using System.Collections.Generic;
using RogueGame.GameSystems.Items;

namespace RogueGame.Maps
{
    public class MapPlanFactory
    {
        public MapPlan Create(MapTemplate template, IDictionary<string, ItemTemplate> items)
        {
            var map = new MapPlan();
            template.FloorItems.ForEach(i => map.FloorItems.Add(items[i]));
            return map;
        }
    }
}