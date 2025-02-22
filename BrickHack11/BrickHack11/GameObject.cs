using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11
{
    abstract class GameObject
    {
        private Texture2D spriteSheet;
        private Rectangle position;
        private Rectangle spriteFrame;

        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle SpriteFrame
        {
            get { return spriteFrame; }
            set { spriteFrame = value; }
        }

        public GameObject(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame)
        {
            this.position = position;
            this.spriteSheet = spriteSheet;
            this.spriteFrame = spriteFrame;
        }

        public virtual void Draw(SpriteBatch sb, Rectangle spriteFrame)
        {
            sb.Draw(spriteSheet, position, spriteFrame, Color.White);
        }
    }
}
