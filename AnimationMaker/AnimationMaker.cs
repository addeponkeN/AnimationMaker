using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Obo.Utility;
using AnimationMaker.Gamer.Screener;
using AnimationMaker.Screener;
using System;
using Obo.GameUtility;

namespace AnimationMaker
{

    public class AnimationMaker : Game
    {
        GraphicsDeviceManager graphics;
        
        public AnimationMaker()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;

            OboGlobals.Load(Globals.ScreenWidth, Globals.ScreenHeight);

            Window.Title = "Animation Maker";
            Window.AllowAltF4 = true;
            Window.AllowUserResizing = false;
            Window.IsBorderless = false;
            Window.AllowUserResizing = false;

            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = false;

            IsFixedTimeStep = true;
            IsMouseVisible = true;

            TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 240);   //240fps

            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            screenManager.AddScreen(new GameScreen());
            
        }

        //protected override void Initialize()
        //{

        //    base.Initialize();
        //}

        //protected override void LoadContent()
        //{
        //    spriteBatch = new SpriteBatch(GraphicsDevice);
        //}

        //protected override void UnloadContent()
        //{

        //}

        //protected override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //}

        //protected override void Draw(GameTime gameTime)
        //{
        //    GraphicsDevice.Clear(new Color(33, 33, 33));

        //    base.Draw(gameTime);
        //}
    }
}
