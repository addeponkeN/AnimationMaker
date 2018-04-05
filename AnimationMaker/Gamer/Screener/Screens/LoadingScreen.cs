﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimationMaker.Screener;
using System;

namespace AnimationMaker.Gamer.Screener
{
    class LoadingScreen : Screen
    {
        bool loadingIsSlow;
        bool otherScreensAreGone;

        Screen[] screensToLoad;
        SpriteFont font;


        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow,
                              Screen[] screensToLoad)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;
            font = GameContent.font;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }
        public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, params Screen[] screensToLoad)
        {
            foreach(Screen screen in screenManager.GetScreens())
                screen.ExitScreen();

            LoadingScreen loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screensToLoad);

            screenManager.AddScreen(loadingScreen);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if(otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach(Screen screen in screensToLoad)
                    if(screen != null)
                        ScreenManager.AddScreen(screen);

                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }

            if(loadingIsSlow)
            {
                const string message = "LOADING..";
                Vector2 textSize = font.MeasureString(message);
                Vector2 textPosition = (Globals.ScreenSize - textSize) / 2;

                spriteBatch.Begin();
                spriteBatch.DrawString(font, message, new Vector2(textPosition.X + 1, textPosition.Y + 1), new Color(35, 35, 35, TransitionAlpha));
                spriteBatch.DrawString(font, message, textPosition, new Color(255, 255, 255, TransitionAlpha));

                spriteBatch.End();
            }
        }
    }
}