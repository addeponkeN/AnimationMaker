using AnimationMaker.Gamer;
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

    public class SelectorGui : Sprite
    {

        static SelectorGui instance = new SelectorGui();
        public static SelectorGui Instance => instance;

        Sprite hoverBox;

        int spriteSize = 32;

        public bool IsHoveringSheet()
        {
            if(selectedSheet == null)
                return false;
            return selectedSheet.IsHovered;
        }

        Spritesheet selectedSheet;
        public Spritesheet SelectedSheet
        {
            get => selectedSheet;
            set
            {
                selectedSheet = new Spritesheet(value.Texture);
                var s = new Vector2(selectedSheet.Texture.Width, selectedSheet.Texture.Height);
                selectedSheet.Size = s;
                //selectedSheet.Position = GHelper.Center(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), selectedSheet.Size);
                selectedSheet.Position = GHelper.Rounder(Position + new Vector2(spriteSize), spriteSize);
            }
        }


        public SelectorGui()
        {
            Position = Gui.SelectorPos;
            Size = Gui.SelectorSize;

            Color = new Color(60, 60, 60);

            hoverBox = new Sprite(GameContent.boxbox);
            hoverBox.SetColor(Color.CornflowerBlue);
            hoverBox.Size = new Vector2(spriteSize);

        }


        public void Update()
        {

            if(IsHoveringSheet())
            {
                hoverBox.Position = GHelper.Rounder(Input.MousePos, (int)(spriteSize));

                if(selectedSheet.IsClicked)
                {
                    var source = hoverBox.Position - selectedSheet.Position;
                    int x = (int)source.X;
                    int y = (int)source.Y;
                    int w = spriteSize;
                    int h = spriteSize;
                    FramesGui.Instance.AddFrame(x, y, w, h);
                }

            }

        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            selectedSheet?.Draw(sb);

            if(IsHoveringSheet())
            {
                hoverBox.Draw(sb);
            }

        }


    }

}
