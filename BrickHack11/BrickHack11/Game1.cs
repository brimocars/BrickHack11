using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickHack11
{
    enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }
    
    public class Game1 : Game
    {
        private GameState _gameState;
        private SpriteManager sprites;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        
        MainMenu mainMenu;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameState = GameState.MainMenu;
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();   
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new SpriteManager(this.Content);
            player = new Player(sprites.PlayerSprite, new Rectangle(100, 100, 64, 64), new Rectangle(0,0,64,64), 3, 3f);
            mainMenu = new MainMenu(sprites.MainMenuTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_gameState)
            {
                case GameState.MainMenu:
                mainMenu.Update();
                    if(mainMenu.playClick == true){
                        _gameState = GameState.Playing;
                    }
                    else if (mainMenu.quitClick == true){
                        Exit();
                    }
					break;
				//case GameState.Playing:
                default :
                    player.Update();
					break;
				case GameState.Paused:
					break;
				case GameState.GameOver:
					break;
				case GameState.Victory:
					break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            // TODO: Add your drawing code here
            player.Draw(_spriteBatch);
            mainMenu.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}