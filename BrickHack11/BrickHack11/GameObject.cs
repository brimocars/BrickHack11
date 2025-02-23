using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BrickHack11
{
    abstract public class GameObject
    {
        private Texture2D spriteSheet;
        private Vector2 position;
        private Rectangle hitbox;
        private Rectangle spriteFrame;
        private Rectangle drawingBox;

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set {
                position = value;
                hitbox = new Rectangle((int)Position.X, (int)Position.Y, hitbox.Width, hitbox.Height);
            }
        }

        public Rectangle Hitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public Rectangle SpriteFrame
        {
            get { return spriteFrame; }
            set { spriteFrame = value; }
        }

        public GameObject(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame, Rectangle drawingBox)
        {
            this.position = position;
            this.hitbox = hitbox;
            this.drawingBox = drawingBox;
            this.spriteSheet = spriteSheet;
            this.spriteFrame = spriteFrame;
        }

        public GameObject(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame) : this(spriteSheet, position, hitbox, spriteFrame, hitbox) {}

        public virtual void Draw(SpriteBatch sb, Rectangle spriteFrame)
        {
            sb.Draw(spriteSheet, position, spriteFrame, Color.White);
        }
    }
}
