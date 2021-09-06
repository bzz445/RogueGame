using RogueGame.Entities;
using RogueGame.Maps;

namespace RogueGame.GameSystems
{
    public interface ITurnBasedGame
    {
        MovingCastlesMap Map { get; set; }
        
        bool HandleAsPlayerInput(SadConsole.Input.Keyboard info);
        void RegisterEntity(McEntity entity);
        void UnregisterEntity(McEntity entity);
        void RegisterPlayer(Player player);
    }
}