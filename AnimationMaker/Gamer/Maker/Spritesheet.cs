using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.GameUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{
    public class Spritesheet : Sprite
    {
        public string Name;

        public int spriteSize = 32;

        public bool IsHovered => Rectangle.Contains(Input.MousePos);
        public bool IsClicked => IsHovered && Input.LeftClick;
        public bool IsReleased => IsHovered && Input.LeftRelease;

        public Spritesheet(Texture2D texture) : base(texture)
        {
            Size = new Vector2(128 - 32, 128 - 32);
        }

        public override void Draw(SpriteBatch sb)
        {
            //sb.Draw(GameContent.boxbox, Rectangle, new Color(Color.PaleGreen, 0.001f));
            base.Draw(sb);

        }

    }
}
