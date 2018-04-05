using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.GameUtility;
using Obo.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{
    public class SpriteFrame
    {
        public static Vector2 frameSize = new Vector2(64, 64);
        public Vector2 position;

        public Rectangle Rectangle => new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)(frameSize.Y));

        public SpriteFrameData Data;

        public int X => Data.X;
        public int Y => Data.Y;
        public int W => Data.W;
        public int H => Data.H;
        public float FrameSpeed => Data.FrameSpeed;

        public Rectangle Source => new Rectangle(X, Y, W, H);

        Label lbSpeed;

        public bool IsDragging;

        public SpriteFrame() { }

        public SpriteFrame(int x, int y, int w, int h)
        {
            Data = new SpriteFrameData(x, y, w, h, 0.25f);
            lbSpeed = new Label(GameContent.font, $"[{FrameSpeed:N2}]");
            SetFrameSpeed(Data.FrameSpeed);
        }

        public SpriteFrame(SpriteFrameData data)
        {
            Data = data;
            lbSpeed = new Label(GameContent.font, $"[{FrameSpeed:N2}]");
            SetFrameSpeed(Data.FrameSpeed);
        }

        public void Update()
        {

            if(lbSpeed.IsHovered || Rectangle.Contains(Input.MousePos))
            {
                if(Input.WheelUp)
                {
                    SetFrameSpeed((float)((double)FrameSpeed + 0.01));
                }
                else if(Input.WheelDown)
                {
                    SetFrameSpeed((float)((double)FrameSpeed - 0.01));
                }

                Input.ScrollValueOld = Input.ScrollValue;
            }

        }

        public void SetFrameSpeed(float val)
        {
            Data.FrameSpeed = val;
            lbSpeed.Text = $"[{FrameSpeed:N2}]";
        }


        public void Draw(SpriteBatch sb, Spritesheet sheet)
        {
            sb.Draw(sheet.Texture, Rectangle, Source, Color.White);
        }

        public void Draw(SpriteBatch sb, Spritesheet sheet, Rectangle drawRec)
        {
            sb.Draw(sheet.Texture, drawRec, Source, Color.White);
        }

        public void DrawInfo(SpriteBatch sb)
        {
            lbSpeed.Position = new Vector2(GHelper.Center(Rectangle, lbSpeed.TextSize).X, Rectangle.Bottom + 16);
            lbSpeed.Draw(sb);
        }
    }
}
