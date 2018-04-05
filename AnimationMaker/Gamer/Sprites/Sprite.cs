using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimationMaker.Gamer
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }
        public void SetPosition(int x, int y)
        {
            Position = new Vector2(x, y);
        }
        public void SetPosition(int xy)
        {
            Position = new Vector2(xy);
        }

        public Point Point => new Point((int)Position.X, (int)Position.Y);

        public Vector2 Size { get; set; }
        public Vector2 BaseSize { get; set; }
        public void SetSize(int x, int y)
        {
            Size = new Vector2(x, y);
            BaseSize = Size;

        }
        public void SetSize(int xy)
        {
            Size = new Vector2(xy);
            BaseSize = Size;
        }

        public Vector2 Direction { get; set; }

        public float Speed { get; set; }
        public float BaseSpeed { get; set; }

        public Rectangle Rectangle => new Rectangle(Position.ToPoint(), Size.ToPoint() * Globals.Scale.ToPoint());
        //public Rectangle Rectangle => new Rectangle(Convertor.ToPoint(Position) - Origin, Convertor.ToPoint(Size * Globals.Scale));

        public int Column { get; set; } = 0;
        public int Row { get; set; } = 0;
        public int SpriteWidth { get; set; } = 256;
        public int SpriteHeight { get; set; } = 256;
        public void SetSourceSize(int width, int heigth)
        {
            SpriteWidth = width;
            SpriteHeight = heigth;
        }
        public void SetSourceSize(int wh)
        {
            SpriteWidth = wh;
            SpriteHeight = wh;
        }
        public Rectangle SourceRectangle => new Rectangle(Column * SpriteWidth, Row * SpriteHeight, SpriteWidth, SpriteHeight);

        public int Alpha { get; set; } = 255;
        public Color Color { get; set; } = Color.White;
        public Color BaseColor { get; set; } = Color.White;
        public void SetColor(Color color)
        {
            Color = color;
            BaseColor = color;
        }

        Color _color => new Color(Color, Alpha);

        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Vector2 LatestDirection { get; set; }

        public void FlipHorizontally()
        {
            if(SpriteEffects == SpriteEffects.FlipHorizontally)
                SpriteEffects = SpriteEffects.None;
            else SpriteEffects = SpriteEffects.FlipHorizontally;
        }

        public void FlipVertically()
        {
            if(SpriteEffects == SpriteEffects.FlipVertically)
                SpriteEffects = SpriteEffects.None;
            else SpriteEffects = SpriteEffects.FlipVertically;
        }

        public float Layer { get; set; } = 0.5f;

        public Sprite()
        {
            Texture = GameContent.box;
        }
        public Sprite(Texture2D texture)
        {
            Texture = texture;
            SpriteWidth = Texture.Width;
            SpriteHeight = Texture.Height;
            SetSize(SpriteWidth, SpriteHeight);
        }

        public void SetFrame(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Rectangle, SourceRectangle, _color, Rotation, Origin, SpriteEffects, Layer);
        }
    }
}
