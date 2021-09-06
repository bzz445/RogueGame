using System.Collections.Generic;
using RogueGame.GameSystems.Items;

namespace RogueGame.Maps
{
    public interface IMapPlan
    {
        /// <summary>
        /// Items that can appear scattered on the floor throughout the map
        /// </summary>
        List<ItemTemplate> FloorItems { get; }
    }
}