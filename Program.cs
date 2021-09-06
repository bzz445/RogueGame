using Autofac;

namespace RogueGame
{
    internal static class Program
    {
        private static void Main()
        {
            var container = AutofacSetup.CreateContainer();
            using var game = container.Resolve<RogueGame>();
            game.Run();
        }
    }
}
