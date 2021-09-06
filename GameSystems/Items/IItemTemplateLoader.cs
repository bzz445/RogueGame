using System.Collections.Generic;

namespace RogueGame.GameSystems.Items
{
    public interface IItemTemplateLoader
    {
        Dictionary<string, ItemTemplate> Load();
    }
}