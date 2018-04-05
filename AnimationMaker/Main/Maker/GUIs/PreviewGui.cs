using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.GameUtility;

namespace AnimationMaker.Gamer
{
    class PreviewGui : Sprite
    {
        static PreviewGui instance = new PreviewGui();
        public static PreviewGui Instance => instance;

        float timer = 0f;
        int frame = 0;

        public SpriteFrame currentFrame;

        public PreviewGui()
        {
            Size = Gui.PrevSize;
            Position = Gui.PrevPos;

            Color = new Color(60, 60, 60);
        }

        public void Update(GameTime gt)
        {
            Size = Gui.PrevSize;
            Position = Gui.PrevPos;

            var delta = gt.Delta();

            var list = FramesGui.Instance.frames;

            if(list != null && list.Count > 0)
            {

                if(currentFrame == null)
                    currentFrame = list[0];

                timer += delta;

                if(timer >= currentFrame.FrameSpeed)
                {
                    frame = (frame + 1) % list.Count;
                    timer = 0;
                }

                if(frame >= list.Count)
                    frame = 0;

                currentFrame = list[frame];

            }
            else currentFrame = null;

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if(currentFrame != null)
            {
                var size = Size * 0.5f;
                var pos = Position + (size * .5f);
                currentFrame.Draw(sb, FramesGui.Instance.SelectedSheet, new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y));

                sb.DrawString(GameContent.font, $"Frame: {frame+1}\nLength: {currentFrame.FrameSpeed - timer:N2}", Position + new Vector2(2), Color.White);
            }

        }

    }
}
