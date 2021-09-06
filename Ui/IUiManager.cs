using RogueGame.GameSystems;
using RogueGame.Maps;
using SadConsole;

namespace RogueGame.Ui
{
    public interface IUiManager
    {
        int ViewPortHeight { get; }
        int ViewPortWidth { get; }

        void ShowMainMenu(IGameManager gameManager);
        ContainerConsole CreateMapScreen(IMapPlan mapPlan, IGameManager gameManager);
    }
}