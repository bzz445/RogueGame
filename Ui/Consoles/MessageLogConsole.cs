using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;

namespace RogueGame.Ui.Consoles
{
    public class MessageLogConsole : ContainerConsole
    {
        private const int _maxLines = 50;

        private readonly Queue<string> _lines;
        private readonly ScrollingConsole _messageConsole;

        public MessageLogConsole(int width, int height, Font font)
        {
            _lines = new Queue<string>();
            _messageConsole = new ScrollingConsole(width, height, font)
            {
                DefaultBackground = ColorHelper.MidnightEstBlue
            };

            Children.Add(_messageConsole);
        }

        public void Add(string message)
        {
            _lines.Enqueue(message);
            if (_lines.Count > _maxLines)
            {
                _lines.Dequeue();
            }

            _messageConsole.Cursor.Position = new Point(1, _lines.Count - 1);

            var coloredMessage = new ColoredString($"> {message}\r\n", new Cell(Color.Gainsboro, ColorHelper.MidnightEstBlue));
            _messageConsole.Cursor.Print(coloredMessage);
        }
    }
}