using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Xna.Framework;
using RogueGame.Components;
using RogueGame.GameSystems.Items;
using SadConsole;
using SadConsole.Controls;
using Console = SadConsole.Console;

namespace RogueGame.Ui
{
    public class InventoryWindow : Window
    {
        private readonly Console _descriptionArea;
        private readonly Button _useButton;
        private readonly Button _closeButton;
        private readonly int _itemButtomWidth;
        private IInventoryItem _selectedItem;

        public InventoryWindow(int width, int height) : base(width, height)
        {
            Contract.Requires(width > 40, "Menu width must be > 200");
            Contract.Requires(width > 10, "Menu width must be > 100");

            _itemButtomWidth = width / 3;
            CloseOnEscKey = true;
            Center();

            _useButton = new Button(7)
            {
                Text = "Use",
                Position = new Point(_itemButtomWidth + 2, height - 2),
            };

            _closeButton = new Button(9)
            {
                Text = "Close",
                Position = new Point(width - 9, height - 2),
            };
            _closeButton.Click += (_, __) => Hide();

            CloseOnEscKey = true;

            var colors = SadConsole.Themes.Library.Default.Colors.Clone();
            _descriptionArea = new Console(width - _itemButtomWidth - 3, height - 4)
            {
                Position = new Point(_itemButtomWidth + 2, 1),
                DefaultBackground = ColorHelper.MidnighterBlue,
            };
            _descriptionArea.Fill(null, ColorHelper.MidnighterBlue, null);

            Children.Add(_descriptionArea);
            Closed += (_, __) => _selectedItem = null;
        }

        public void Show(IInventoryComponent inventory)
        {
            var controls = BuildItemControls(inventory.Items);
            RefreshControls(controls);

            base.Show(true);
        }

        public override void Update(TimeSpan time)
        {
            _descriptionArea.Clear();
            _descriptionArea.Cursor.Position = new Point(0, 0);
            _descriptionArea.Cursor.Print(new ColoredString(
                _selectedItem?.Description ?? string.Empty,new Cell(_descriptionArea.DefaultForeground, _descriptionArea.DefaultBackground)));
            base.Update(time);
        }

        private List<ControlBase> BuildItemControls(IEnumerable<IInventoryItem> items)
        {
            var yCount = 0;
            return items.Select(i =>
            {
                var itemButton = new Button(_itemButtomWidth - 1)
                {
                    Text = TruncateName(i.Name, _itemButtomWidth - 5),
                    Position = new Point(0, yCount++),
                };
                itemButton.Click += (_, __) => _selectedItem = i;
                return itemButton;
            }).ToList<ControlBase>();
        }

        private void RefreshControls(List<ControlBase> controls)
        {
            RemoveAll();

            Add(_useButton);
            Add(_closeButton);

            foreach (var control in controls)
            {
                Add(control);
            }
        }

        private string TruncateName(string name, int maxLen)
        {
            if (name.Length > maxLen)
            {
                return name.Substring(0, maxLen - 3) + "...";
            }

            return name;
        }
    }
}