using System;
using BrickHack11.Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        private GameState _previousGameState;
        private SpriteManager sprites;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;

        private List<Bullet> _bullets;
        
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
            _bullets = new List<Bullet>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new SpriteManager(this.Content);

            var startingPosition = new Vector2(100, 100);
            
            _player = new Player(
                sprites.PlayerSprite, 
                new Vector2(startingPosition.X, startingPosition.Y), 
                new Rectangle((int)startingPosition.X, (int)startingPosition.Y, 64, 64), 
                new Rectangle(0,0,64,64), 3, 3f);
            
            mainMenu = new MainMenu(sprites.MainMenuTexture, sprites.PlayButtonTexture, sprites.ExitButtonTexture);
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
                    
                    _previousGameState = GameState.MainMenu;
					break;
                
                
				case GameState.Playing:
                    if (_previousGameState == GameState.MainMenu)
                    {
                        var pattern = new CirclePattern(100, 300f);
                        pattern.Spawn(new Vector2(300, 500), 
                            sprites.PlayerSprite, 
                            new Rectangle(0, 0, 10, 10),
                            _bullets);
                    }

                    foreach (var bullet in _bullets)
                    {
                        bullet.Update(gameTime);
                        // Check collision:
                        if (_player._parryBound.Intersects(bullet.Hitbox))
                        {
                            _player.setParry(true, bullet);
                        }
                    }
                    
                    _player.Update();
                    // Check for parry:
                    KeyboardState state = Keyboard.GetState();
                    if(state.IsKeyDown(Keys.Space) && _player._canParry)
                    {
                        // PARRY!!!
                        //_player._bulletToParry.;
                    }

                    _previousGameState = GameState.Playing;
					break;
                
                
				case GameState.Paused:
                    _previousGameState = GameState.Paused;
					break;
                
                
				case GameState.GameOver:
                    _previousGameState = GameState.GameOver;
					break;
                
                
				case GameState.Victory:
                    _previousGameState = GameState.Victory;
					break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            switch (_gameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(_spriteBatch);
                    break;
                
                
                case GameState.Playing:
                    _player.Draw(_spriteBatch);
                    foreach (Bullet bullet in _bullets)
                    {  
                        bullet.Draw(_spriteBatch);
                    }
                    break;
                
                
                case GameState.Paused:
                    break;
                
                
                case GameState.GameOver:
                    break;
                
                
                case GameState.Victory:
                    break;
            }

            // TODO: Add your drawing code here

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    
}