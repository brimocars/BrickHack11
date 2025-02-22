using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11
{
    public class SpriteManager
    {
        private Texture2D playerSprite;
        public Texture2D PlayerSprite { get => playerSprite; }

        public SpriteManager(ContentManager content)
        {
            playerSprite = content.Load<Texture2D>("ball");
        }
    }



}
