using Autofac;
using RogueGame.GameSystems;
using RogueGame.GameSystems.Items;
using RogueGame.Maps;
using RogueGame.Ui;

namespace RogueGame
{
    public static class AutofacSetup
    {
        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UiManager>()
                .As<IUiManager>();
            builder.RegisterType<GameManager>()
                .As<IGameManager>();
            builder.RegisterType<ItemTemplateLoader>()
                .As<IItemTemplateLoader>();
            builder.RegisterType<MapTemplateLoader>()
                .As<IMapTemplateLoader>();
            
            builder.RegisterType<RogueGame>().AsSelf();
            return builder.Build();
        }
    }
}