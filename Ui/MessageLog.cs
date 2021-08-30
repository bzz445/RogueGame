﻿using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueGame.Ui
{
    public class MessageLog : ContainerConsole
    {
        private const int _maxLines = 50;

        private readonly Queue<string> _lines;
        private readonly ScrollingConsole _messageConsole;

        public MessageLog(int width, int height, Font font)
        {
            _lines = new Queue<string>();
            _messageConsole = new ScrollingConsole(width, height, font);
            _messageConsole.DefaultBackground = UiManager.MidnightEstBlue;
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

            var coloredMessage = new ColoredString(message + "\n", new Cell(Color.Gainsboro, UiManager.MidnightEstBlue));
            _messageConsole.Cursor.Print(coloredMessage);
        }
    }
}