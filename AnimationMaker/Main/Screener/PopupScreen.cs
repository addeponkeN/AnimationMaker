using AnimationMaker.Screener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Obo.GameUtility;
using Obo.Gui;
using Obo.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer.Screener
{
    public enum PopupType
    {
        Ok,
        YesNo,
    }

    public enum PopupAnswer
    {
        None,
        Ok,
        Cancel,
        Yes,
        No,
    }

    public class PopupScreen : Screen
    {

        Sprite box, outline;

        Button btOk, btYes, btNo, btCancel;
        Label lbText;

        PopupType pType;

        string msg;

        SpriteFont font = UtilityContent.debugFont;
        Color Color;

        public PopupScreen(string text, bool error = false, PopupType type = PopupType.Ok)
        {
            IsPopup = true;
            pType = type;
            if(error)
                Color = Color.DarkRed;
            else
                Color = Color.ForestGreen;
            msg = text;
        }

        public PopupScreen(string text, Color color, PopupType type = PopupType.Ok)
        {
            IsPopup = true;
            pType = type;
            Color = color;
            msg = text;
        }

        void CreateBox()
        {
            box = new Sprite();
            box.Texture = Extras.CreateFilledBox(ScreenManager.GraphicsDevice);
            box.SetSize(224, 96);
            box.Position = GHelper.Center(Globals.ScreenBox, box.Size) - new Vector2(0, box.Size.Y * .5f);
            box.SetColor(Color);

            outline = new Sprite();
            outline.Texture = Extras.CreateFilledBox(ScreenManager.GraphicsDevice);
            outline.SetSize((int)box.Size.X + 4, (int)box.Size.Y + 4);
            outline.Position = box.Position - new Vector2(2, 2);
            outline.SetColor(Color.Black);
        }

        void AddButtons()
        {

            switch(pType)
            {
                case PopupType.Ok:
                    btOk = new Button(ScreenManager.GraphicsDevice, 64, 32, "Ok");
                    btOk.Position = new Vector2(GHelper.Center(box.Rectangle, btOk.Size).X, box.Rectangle.Bottom - btOk.Size.Y - 4);
                    break;
                case PopupType.YesNo:
                    btYes = new Button(ScreenManager.GraphicsDevice, 64, 32, "Yes");
                    btYes.Position = new Vector2(box.Position.X + 4, box.Rectangle.Bottom - btYes.Size.Y - 4);

                    btNo = new Button(ScreenManager.GraphicsDevice, 64, 32, "No");
                    btNo.Position = new Vector2(box.Position.X + box.Size.X - btNo.Size.X - 4, box.Rectangle.Bottom - btNo.Size.Y - 4);
                    break;
                default:
                    break;
            }
        }

        public override void Load()
        {
            base.Load();

            CreateBox();
            AddButtons();

            lbText = new Label(font, msg);
            lbText.Position = new Vector2(GHelper.Center(box.Rectangle, lbText.TextSize).X, box.Position.Y + 4);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            switch(pType)
            {
                case PopupType.Ok:

                    if(btOk.IsReleased || Input.KeyClick(Keys.Escape) || Input.KeyClick(Keys.Enter))
                    {
                        GiveAnswer(PopupAnswer.Ok);
                        ExitScreen();
                    }

                    break;
                case PopupType.YesNo:

                    if(btYes.IsReleased)
                    {
                        GiveAnswer(PopupAnswer.Yes);
                        ExitScreen();
                    }

                    if(btNo.IsReleased)
                    {
                        GiveAnswer(PopupAnswer.No);
                        ExitScreen();
                    }

                    break;
                default:
                    break;
            }

        }

        public void ExitScreen()
        {
            IsExiting = true;
        }

        public override void Draw(SpriteBatch sb, GameTime gt)
        {
            base.Draw(sb, gt);

            sb.Begin();

            outline.Draw(sb);
            box.Draw(sb);
            lbText.Draw(sb);

            btOk?.Draw(sb);
            btYes?.Draw(sb);
            btNo?.Draw(sb);
            btCancel?.Draw(sb);

            sb.End();
        }

    }
}
