using AnimationMaker.Screener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationMaker.Gamer
{
    class GameContent
    {
        public static SpriteFont font;

        public static Texture2D box, boxbox, boxhalf;

        static ContentManager content;

        public static void LoadFonts(ContentManager c)
        {
            font = content.Load<SpriteFont>("Fonts/debugFont");
        }

        public static void Load(ContentManager c)
        {
            content = c;
            box = Texture("box");
            boxbox = Texture("boxbox");
            boxhalf = Texture("boxhalf");
            LoadFonts(c);
        }

        public static void LoadAll(ContentManager c)
        {
            Load(c);
        }

        public static void Unload()
        {
            content.Unload();
            content.Dispose();
            content = null;
        }

        #region CustomLoaders

        // load texture2d
        static Texture2D Texture(string path)
        {
            return content.Load<Texture2D>("Textures/" + path);
        }

        // load sound
        static SoundEffect Sound(string path)
        {
            return content.Load<SoundEffect>(path);
        }

        static SpriteFont Font(string path)
        {
            return content.Load<SpriteFont>("Fonts/" + path);
        }

        #endregion


        public static Texture2D LoadRawTexture(string path)
        {
            Texture2D texture;
            using(FileStream stream = new FileStream(path, FileMode.Open))
            {
                texture = Texture2D.FromStream(ScreenManager.gd, stream);
                stream.Dispose();
                stream.Close();
            }
            return texture;
        }

        public static Texture2D DrawRectangle(GraphicsDevice gd)
        {
            Texture2D Texture = new Texture2D(gd, 1, 1);
            Color[] colorData = { new Color(Color.White, (int)255) };
            Texture.SetData(colorData);
            return Texture;
        }

        public static Texture2D DrawRectangle(GraphicsDevice gd, int width, int height)
        {
            Texture2D Texture = new Texture2D(gd, width, height);
            Color[] colorData = { new Color(Color.White, (int)255) };
            Texture.SetData(colorData);
            return Texture;
        }
    }
}
