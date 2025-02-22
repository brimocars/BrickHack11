using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11
{
    public class SpriteManager
    {
        private Texture2D mainMenuTexture;
        private Texture2D playerSprite;

        private Texture2D playButtonTexture;
        private Texture2D exitButtonTexture;
        public Texture2D PlayerSprite { get => playerSprite; }
        
        public Texture2D MainMenuTexture { get => mainMenuTexture; }
        public Texture2D PlayButtonTexture { get =>playButtonTexture;}
        public Texture2D ExitButtonTexture { get =>exitButtonTexture;}
        public SpriteManager(ContentManager content)
        {
            playerSprite = content.Load<Texture2D>("ball");
            mainMenuTexture = content.Load<Texture2D>("beanImage");
            playButtonTexture = content.Load<Texture2D>("playButton");
            exitButtonTexture = content.Load<Texture2D>("quitButton");
        }
    }



}
