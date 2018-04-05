using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{
    class Gui
    {
        public static int Spacing = 8;     // screen bounds spacing

        public static int leftWidth => Globals.ScreenWidth / 5;
        public static int rightWidth => (Globals.ScreenWidth / 5) - (Spacing * 2);
        public static int midWidth => Globals.ScreenWidth - (leftWidth + rightWidth) - (Spacing * 4);

        // Globals.ScreenWidth - BSpacing - PrevSize.X

        static int leftCol = Spacing;
        static int midCol = leftWidth + (Spacing * 2);
        static int rightCol = midCol + midWidth + Spacing;

        static int mid = 128;
        static int bot = 384;

        public static Vector2 MenuPos = new Vector2(Spacing);
        public static Vector2 MenuSize = new Vector2(leftWidth, mid + 256 - (Spacing * 2));

        public static Vector2 PrevSize = new Vector2(rightWidth);
        public static Vector2 PrevPos = new Vector2(rightCol, Spacing);

        public static Vector2 ListPos = new Vector2(Spacing, bot);
        public static Vector2 ListSize = new Vector2(leftWidth, Globals.ScreenHeight - bot - Spacing);

        public static Vector2 SelectorPos = new Vector2(Spacing + ListSize.X + ListPos.X, bot);
        public static Vector2 SelectorSize = new Vector2(midWidth, Globals.ScreenHeight - bot - Spacing);

        public static Vector2 FramesPos = new Vector2(SelectorPos.X, Spacing + mid);
        public static Vector2 FramesSize = new Vector2(midWidth, 256 - (Spacing * 2));

        public static Vector2 AnimListPos = new Vector2(rightCol, bot);
        public static Vector2 AnimListSize = new Vector2(rightWidth, SelectorSize.Y);

    }
}
