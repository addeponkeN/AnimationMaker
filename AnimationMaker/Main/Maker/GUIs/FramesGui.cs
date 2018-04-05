using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Obo.Gui;
using System.Collections.Generic;
using AnimationMaker.Screener;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Obo.GameUtility;

namespace AnimationMaker.Gamer
{
    class FramesGui : Sprite
    {
        static FramesGui instance = new FramesGui();
        public static FramesGui Instance => instance;

        public Spritesheet SelectedSheet;
        public List<SpriteFrame> frames = new List<SpriteFrame>();
        public bool AnyDragging => frames.Any(x => x.IsDragging);
        public SpriteFrame frame;
        public int index = 0;

        AnimationData currentData;

        Sprite selectBox;
        Sprite hoverBox;

        Button btReset;

        Label lbSpeed;
        public InputBox ibSpeed;

        Vector2 clickPos = Vector2.Zero;
        bool prepareDrag;
        int prepareIndex;


        public FramesGui()
        {
            Size = Gui.FramesSize;
            Position = Gui.FramesPos;

            Color = new Color(60, 60, 60);

            selectBox = new Sprite(GameContent.boxbox);
            selectBox.SetColor(Color.CornflowerBlue);
            selectBox.Size = SpriteFrame.frameSize;

            hoverBox = new Sprite(GameContent.box);
            hoverBox.SetColor(new Color(20, 20, 20));
            hoverBox.Size = SpriteFrame.frameSize;
            hoverBox.Alpha = 25;

            btReset = CreateBt(Position.X + 4, Position.Y + 4 + 24, "Reset");
            btReset.SetColor(new Color(50, 50, 50));

            ibSpeed = new InputBox(ScreenManager.gd, 80, 24);
            ibSpeed.Position = Position + new Vector2(4) + new Vector2(128, 24);

            lbSpeed = new Label(GameContent.font, "FrameLength");
            lbSpeed.Position = new Vector2(GHelper.Center(ibSpeed.Rectangle, lbSpeed.TextSize).X, ibSpeed.Position.Y - lbSpeed.TextSize.Y - 2);
        }

        Button CreateBt(float x, float y, string text)
        {
            return new Button(ScreenManager.gd, new Vector2(96, 26), text) { Position = new Vector2(x, y) };
        }

        public void AddFrame(int x, int y, int w, int h)
        {
            var sf = new SpriteFrame(x, y, w, h);
            frames.Add(sf);

            var animList = AnimationsListGui.Instance;

            if(currentData.AnimationName == null)
            {
                animList.AddNewLabel();
                currentData = animList.Labels.First().data;
            }

            currentData.Frames.Add(new SpriteFrameData(x, y, w, h, sf.FrameSpeed));
        }

        public void SetFrames(AnimationData data)
        {
            Clear();
            currentData = data;

            foreach(var f in data.Frames)
            {
                frames.Add(new SpriteFrame(f));
            }

        }

        public void RemoveFrame(int i)
        {
            frames.RemoveAt(i);
            currentData.Frames.RemoveAt(i);

            if(index > frames.Count - 1)
                index = frames.Count - 1;
            if(index < 0)
                index = 0;

            if(frames.Count == 0)
                frame = null;
            else
                frame = frames[index];
        }

        void Select(int i)
        {
            index = i;
            frame = frames[i];

            ibSpeed.Text = $"{frame.FrameSpeed:N2}";
        }

        public void Update(GameTime gt)
        {
            if(Input.LeftClick)
            {
                clickPos = Vector2.Zero;
                prepareDrag = false;
            }
            for(int i = 0; i < frames.Count; i++)
            {
                var f = frames[i];
                f.Update();

                if(!AnyDragging)
                {
                    if(f.Rectangle.Contains(Input.MousePos) && Input.LeftClick)
                    {
                        prepareDrag = true;
                        clickPos = Input.MousePos;
                        prepareIndex = i;
                    }

                    if(prepareDrag)
                        if(Input.LeftHold)
                        {
                            var distance = Vector2.Distance(clickPos, Input.MousePos);
                            if(distance > 20)
                            {
                                frames[prepareIndex].IsDragging = true;
                                prepareDrag = false;
                                clickPos = Vector2.Zero;
                            }
                        }
                }

                if(f.IsDragging)
                {
                    f.position = Input.MousePos - (SpriteFrame.frameSize / 2);
                    if(Input.LeftRelease)
                    {
                        for(int j = 0; j < frames.Count; j++)
                        {
                            if(i == j)
                                continue;

                            if(frames[j].Rectangle.Contains(Input.MousePos))
                            {
                                //  swap
                                var temp = f;
                                frames[i] = frames[j];
                                frames[j] = temp;
                                index = j;
                                break;
                            }

                        }

                        f.IsDragging = false;
                    }
                }

                if(Input.LeftClick)
                {
                    if(f.Rectangle.Contains(Input.MousePos))
                    {
                        Select(i);
                    }
                }

                if(Input.RightClick)
                {
                    if(f.Rectangle.Contains(Input.MousePos))
                    {
                        RemoveFrame(i);
                    }
                }

            }

            if(btReset.IsClicked)
            {
                Clear();
            }

            if(frame != null)
            {
                ibSpeed.Update(gt);

                if(Input.KeyClick(Keys.Delete))
                {
                    RemoveFrame(index);
                }


                if(ibSpeed.IsSelected)
                {
                    if(Input.KeyClick(Keys.Enter))
                    {
                        try
                        {
                            ibSpeed.Text = ibSpeed.Text.Replace('.', ',');
                            SetFrameSpeed(index, float.Parse(ibSpeed.Text));
                        }
                        catch
                        {
                            System.Console.WriteLine("parse fail");
                        }
                        

                        ibSpeed.IsSelected = false;
                    }
                }
            }
            else
                ibSpeed.IsSelected = false;

        }

        void SetFrameSpeed(int i, float speed)
        {
            frame.SetFrameSpeed(speed);
            currentData.Frames[i].FrameSpeed = speed;
        }

        void Clear()
        {
            frames.Clear();
            frame = null;
            index = 0;

            PreviewGui.Instance.currentFrame = null;
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            int i = 0;

            foreach(var f in frames)
            {
                var spacing = 8;
                int y = (int)GHelper.Center(Rectangle, SpriteFrame.frameSize).Y;
                int x = (int)Position.X + spacing + (i * (int)SpriteFrame.frameSize.X) + (i * 16);
                var pos = new Vector2(x, y);

                if(!f.IsDragging)
                {
                    f.position = pos;
                    if(f.Rectangle.Contains(Input.MousePos))
                    {
                        hoverBox.Position = f.position;
                        hoverBox.Draw(sb);
                    }
                }

                f.Draw(sb, SelectedSheet);
                f.DrawInfo(sb);

                sb.DrawString(GameContent.font, $"{i + 1}", pos - new Vector2(10), Color.White);

                i++;
            }

            if(frame != null)
            {
                selectBox.Position = frame.position;
                selectBox.Draw(sb);
            }


            btReset.Draw(sb);

            ibSpeed.Draw(sb);
            lbSpeed.Draw(sb);
        }


    }
}
