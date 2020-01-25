using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCodeur
{
    public class Sprite : IActor
    {
        // IActor
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public float vX;
        public float vY;

        // Sprite
        public Texture2D Texture { get; }
        public bool ToRemove { get; set; }

        public Sprite(Texture2D pTexture)
        {
            Texture = pTexture;
            ToRemove = false;
        }

        public void Move(float pX, float pY)
        {
            Position = new Vector2(Position.X + pX, Position.Y + pY);
        }
        

        public virtual void TouchedBy(IActor pBy)
        {

        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update(GameTime pGameTime)
        {
            Move(vX, vY);
            BoundingBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height);
        }
    }
}
