using AnimationMaker.Screener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Obo.GameUtility;
using Obo.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{

    class AnimationLabel : Label
    {
        public static int TextHeight;

        public AnimationData data;

        new public Rectangle Rectangle => new Rectangle((int)Position.X - 4, (int)Position.Y, Gui.rightWidth, TextHeight);

        new public bool IsHovered => Rectangle.Contains(Input.MousePos);
        new public bool IsClicked => IsHovered && Input.LeftClick;

        public AnimationLabel(AnimationData dt) : base(UtilityContent.debugFont, dt.AnimationName)
        {
            data = dt;
            IsEditable = true;
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);

            if(!IsEditing && string.IsNullOrEmpty(Text))
                Text = BaseText;

            if(Input.KeyClick(Keys.Enter))
                IsEditing = false;

            if(IsEditing)
            {
                data.AnimationName = Text;
            }


        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

    }

    class AnimationsListGui : Sprite
    {

        static AnimationsListGui instance = new AnimationsListGui();
        public static AnimationsListGui Instance => instance;

        public List<AnimationLabel> Labels = new List<AnimationLabel>();

        Sprite hoverBox;
        Sprite selectedBox;

        Button btAdd;
        Button btDel;

        public int selectedIndex = -1;

        static int count;
        const int labelSpacing = 18;

        bool anyHovered;


        Vector2 LabelBasePos => Position + new Vector2(4, 4 + 40);

        public AnimationsListGui()
        {
            Size = Gui.AnimListSize;
            Position = Gui.AnimListPos;

            Color = new Color(60, 60, 60);

            var textSize = UtilityContent.debugFont.MeasureString("label #");
            AnimationLabel.TextHeight = (int)textSize.Y;

            selectedBox = new Sprite(Extras.CreateHollowBox(ScreenManager.gd, Gui.rightWidth, (int)textSize.Y, 1));
            //selectedBox.Size = new Vector2(Gui.rightWidth, textSize.Y);
            selectedBox.Color = Color.LightSteelBlue;
            selectedBox.Alpha = 100;

            hoverBox = new Sprite();
            hoverBox.Size = new Vector2(Gui.rightWidth, textSize.Y);
            hoverBox.Color = new Color(80, 80, 80);
            hoverBox.Alpha = 50;

            btAdd = new Button(ScreenManager.gd, 42, 32, "Add");
            btAdd.SetColor(new Color(50, 50, 50));
            btAdd.Position = new Vector2(GHelper.Center(Rectangle, btAdd.Size).X - 32, Position.Y + 4);

            btDel = new Button(ScreenManager.gd, 42, 32, "Del");
            btDel.SetColor(new Color(50, 50, 50));
            btDel.Position = new Vector2(GHelper.Center(Rectangle, btDel.Size).X + 32, Position.Y + 4);

        }

        public void AddLabel(AnimationLabel label)
        {
            SetupLabel(label);
            Labels.Add(label);
            if(Labels.Count == 1)
            {
                hoverBox.Position = Labels[0].Position - new Vector2(4, 0);
                selectedBox.Position = Labels[0].Position - new Vector2(4, 0);
            }
        }

        void SetupLabel(AnimationLabel lb)
        {
            lb.Position = LabelBasePos + new Vector2(0, (Labels.Count * labelSpacing));
        }

        public void AddNewLabel()
        {
            AnimationLabel lb = new AnimationLabel(new AnimationData("Animation #" + count++, new List<SpriteFrameData>()));
            AddLabel(lb);
        }

        public void LoadFile(AnimationExportFile file)
        {
            foreach(var a in file.List)
            {
                AddLabel(new AnimationLabel(a));
            }
        }

        public void Update(GameTime gt)
        {

            if(btAdd.IsReleased)
                AddNewLabel();

            anyHovered = false;
            for(int i = 0; i < Labels.Count; i++)
            {
                var l = Labels[i];
                l.Update(gt);
                                

                if(l.IsClicked)
                {
                    Select(i);
                }

                if(l.IsHovered)
                {
                    hoverBox.Position = l.Position - new Vector2(4, 0);
                    anyHovered = true;
                }
            }

        }

        void Select(int i)
        {
            selectedIndex = i;
            selectedBox.Position = Labels[i].Position - new Vector2(4, 0);

            FramesGui.Instance.SetFrames(Labels[i].data);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            btAdd.Draw(sb);
            btDel.Draw(sb);

            if(Labels.Count > 0)
            {
                if(anyHovered)
                hoverBox.Draw(sb);
                selectedBox.Draw(sb);
            }

            for(int i = 0; i < Labels.Count; i++)
            {
                var lb = Labels[i];
                lb.Draw(sb);
            }

        }
    }
}
