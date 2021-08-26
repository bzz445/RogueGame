namespace RogueGame
{
    internal static class Program
    {
        private static void Main()
        {
            using var game = new RogueGame();
            game.Run();
        }
    }
}
