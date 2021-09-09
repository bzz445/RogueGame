using RogueGame.GameSystems;
using RogueGame.Maps;
using RogueGame.Ui.Consoles;
using SadConsole;

namespace RogueGame.Ui
{
    public interface IUiManager
    {
        int ViewPortHeight { get; }
        int ViewPortWidth { get; }

        void ShowMainMenu(IGameManager gameManager);
        ContainerConsole CreateDungeonMapScreen(IMapPlan mapPlan, IGameManager gameManager);
        CastleModeConsole CreateCastleMapScreen(IMapPlan mapPlan, IGameManager gameManager);
    }
}