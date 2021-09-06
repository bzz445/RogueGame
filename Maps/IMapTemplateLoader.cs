using System.Collections.Generic;

namespace RogueGame.Maps
{
    public interface IMapTemplateLoader
    {
        Dictionary<string, MapTemplate> Load();
    }
}