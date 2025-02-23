using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11
{
    public class SpriteManager
    {
        private Texture2D mainMenuTexture;
        private Texture2D playerSprite;
        private Texture2D shieldSprite;
        private Texture2D bulletSprite;
        private Texture2D enemySprite;
        private Texture2D playButtonTexture;
        private Texture2D exitButtonTexture;
        private Texture2D playerHealthIcon;
        private Texture2D enemyHealthIcon;

        public Texture2D PlayerSprite { get => playerSprite; }
        public Texture2D ShieldSprite { get => shieldSprite; }
        public Texture2D BulletSprite { get => bulletSprite; }
        public Texture2D EnemySprite { get => enemySprite; }
        public Texture2D MainMenuTexture { get => mainMenuTexture; }
        public Texture2D PlayButtonTexture { get => playButtonTexture;}
        public Texture2D ExitButtonTexture { get => exitButtonTexture;}
        public Texture2D PlayerHealthIcon { get => playerHealthIcon; }
        public Texture2D EnemyHealthIcon { get => enemyHealthIcon; }
        public SpriteManager(ContentManager content)
        {
            bulletSprite = content.Load<Texture2D>("ball");
            enemySprite = content.Load<Texture2D>("enemy1");
            playerSprite = content.Load<Texture2D>("player");
            shieldSprite = content.Load<Texture2D>("shield");
            mainMenuTexture = content.Load<Texture2D>("beanImage");
            playButtonTexture = content.Load<Texture2D>("playButton");
            exitButtonTexture = content.Load<Texture2D>("quitButton");
            playerHealthIcon = content.Load<Texture2D>("ball");
            enemyHealthIcon = content.Load<Texture2D>("ball");

        }
    }



}
