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
        private Texture2D ballTexture;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new SpriteManager(this.Content);
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
				case GameState.Playing:
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }

    
}