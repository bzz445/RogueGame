using System.Collections.Generic;
using SadConsole;

namespace RogueGame.Consoles
{
    public class ConsoleListEventArgs: System.EventArgs
    {
        public ConsoleListEventArgs(List<Console> consoles)
        {
            Consoles = consoles;
        }
        
        public List<Console> Consoles { get; }
    }
}