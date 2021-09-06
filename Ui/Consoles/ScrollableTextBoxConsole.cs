using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Input;

namespace RogueGame.Ui.Consoles
{
    public class ScrollableTextBoxConsole : ScrollingConsole
    {
        private readonly ControlsConsole _controlsContainer;
        private readonly ScrollBar _scrollBar;

        /// <summary>Scroll bar position</summary>
        public int ScrollOffset { get; private set; } = 0;

        public ScrollableTextBoxConsole(int width, int height, int bufferHeight) :
            base(
                width: width - 1,
                height: bufferHeight,
                font: Global.FontDefault,
                viewPort: new Rectangle(0, 0, width - 1, height)
            )
        {
            _controlsContainer = new ControlsConsole(1, height);
            ViewPort = new Rectangle(0, 0, width, height);

            _scrollBar = new ScrollBar(Orientation.Vertical, height);
            _scrollBar.IsEnabled = true;
            _scrollBar.ValueChanged += ScrollBar_ValueChanged;

            _controlsContainer.Add(_scrollBar);
            _controlsContainer.Position = new Point(Position.X + width - 1, Position.Y);
            _controlsContainer.IsVisible = true;

            Cursor.IsVisible = false;
            Cursor.UseStringParser = true;
        }

        public void SetText(string text)
        {
            Clear();
            Cursor.Position = new Point(0, 0);
            Cursor.Print(text);
        }

        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);
            // If cursor position exceeds our displayable content viewport, 
            // move the ScrollOffset automatically to display new content.
            if (TimesShiftedUp != 0 | Cursor.Position.Y >= ViewPort.Height + ScrollOffset)
            {
                // Scollbar has to be enabled to read previous content.
                _scrollBar.IsEnabled = true;

                // Cursor offset cannot exceed our viewable data end row.
                // Think about it, we would reach infinity and empty space D:
                if (ScrollOffset < Height - ViewPort.Height)
                {
                    // Automatically calculate our content viewport by scrolling the cursor
                    // Based on how much content is inaccessible.
                    ScrollOffset += TimesShiftedUp != 0 ? TimesShiftedUp : 1;
                }

                _scrollBar.Maximum = Height + ScrollOffset - Height;

                // This will follow the cursor since we move the render area in the event.
                _scrollBar.Value = ScrollOffset;

                // Reset the shift amount.
                TimesShiftedUp = 0;
            }
        }

        public override bool ProcessMouse(MouseConsoleState state)
        {
            // Create a state based on our container that has the scroll bar.
            var stateForScroll = new MouseConsoleState(_controlsContainer, state.Mouse);

            // Check if this state based on the console holding the scroll bar.
            if (stateForScroll.IsOnConsole)
            {
                _controlsContainer.ProcessMouse(stateForScroll);
                return true;
            }

            // If we're here, continue the mouse processing flow ordinarily.
            return base.ProcessMouse(state);
        }

        protected override void OnPositionChanged(Point oldLocation)
        {
            // Keep the controls console (which is our scroll bar) in sync with where this console is.
            _controlsContainer.Position = new Point(Position.X + Width, Position.Y);
        }

        protected override void OnVisibleChanged()
        {
            _controlsContainer.IsVisible = IsVisible;
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            // Display viewable content based on our scroll offset.
            ViewPort = new Rectangle(0, _scrollBar.Value, Width, ViewPort.Height);
        }
    }
}