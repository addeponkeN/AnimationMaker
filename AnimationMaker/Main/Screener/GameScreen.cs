using AnimationMaker.Screener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationMaker.Gamer;
using Microsoft.Xna.Framework.Content;

namespace AnimationMaker.Gamer.Screener
{
    class GameScreen : Screen
    {

        SpritesheetListGui sheetList;
        SelectorGui selector;
        FramesGui frames;
        PreviewGui prev;
        MenuGui menu;
        AnimationsListGui animList;

        public GameScreen()
        {

        }

        public override void Load()
        {
            base.Load();

            if(Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");

            GameContent.Load(Content);

            menu = MenuGui.Instance;
            sheetList = SpritesheetListGui.Instance;
            selector = SelectorGui.Instance;
            frames = FramesGui.Instance;
            prev = PreviewGui.Instance;
            animList = AnimationsListGui.Instance;

        }

        public override void LoadAfterLoad()
        {
            base.LoadAfterLoad();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void ActiveUpdate(GameTime gt)
        {
            base.ActiveUpdate(gt);

            menu.Update(gt, this);
            sheetList.Update();
            selector.Update();
            frames.Update(gt);
            prev.Update(gt);
            animList.Update(gt);
        }

        public override void Draw(SpriteBatch sb, GameTime gt)
        {
            base.Draw(sb, gt);

            sb.Begin();

            sheetList.Draw(sb);
            selector.Draw(sb);
            prev.Draw(sb);
            menu.Draw(sb);
            animList.Draw(sb);
            frames.Draw(sb);

            sb.End();
        }

    }
}
