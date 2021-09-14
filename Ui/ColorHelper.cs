using Microsoft.Xna.Framework;
using SadConsole.Themes;

namespace RogueGame.Ui
{
    public static class ColorHelper
    {
        public static Color PlayerBlue = new Color(140, 180, 190);
        public static Color MidnightEstBlue = new Color(3, 3, 15);
        public static Color MidnighterBlue = new Color(5, 5, 25);
        public static Color ManaBlue = new Color(45, 105, 175);
        public static Color DepletedManaBlue = new Color(10, 25, 45);
        public static Color HealthRed = new Color(135, 0, 0);
        public static Color DepletedHealthRed = new Color(30, 0, 0);
        public static Color WhiteHighlight = new Color(255, 255, 255, 150);
        public static Color GreyHighlight = new Color(100, 100, 100, 100);
        
        public static Colors GetThemeColorsForBackgroundColor(Color bgColor)
        {
            var colors = Library.Default.Colors.Clone();

            colors.ControlBack = bgColor;
            colors.ControlBackLight = bgColor;
            colors.ControlBackSelected = bgColor;
            colors.ControlBackDark = bgColor;
            colors.ControlHostBack = bgColor;

            colors.RebuildAppearances();

            return colors;
        }

        public static Colors GetProgressBarThemeColors(Color back, Color fore)
        {
            var colors = Library.Default.Colors.Clone();

            colors.Text = fore;

            colors.ControlBack = back;
            colors.ControlBackLight = back;
            colors.ControlBackSelected = back;
            colors.ControlBackDark = back;
            colors.ControlHostBack = back;

            colors.RebuildAppearances();

            return colors;
        }
    }
}