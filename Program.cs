namespace RogueGame
{
    internal class Program
    {
        private static void Main()
        {
            using var game = new RogueGame();
            game.Run();
        }
    }
}
