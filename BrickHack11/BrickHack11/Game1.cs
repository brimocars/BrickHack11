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
            _player = new Player(sprites.PlayerSprite, new Rectangle(100, 100, 64, 64), new Rectangle(0,0,64,64), 3, 3f);
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
                    
                    _gameState = GameState.Playing;
                    _previousGameState = GameState.MainMenu;
					break;
				case GameState.Playing:
                    if (_previousGameState == GameState.MainMenu)
                    {
                        var pattern = new StreamPattern(300, 1.0f, 1.0f);
                        pattern.Spawn(new Vector2(100, 100), 
                            sprites.PlayerSprite, 
                            new Rectangle(0, 0, 0, 0),
                            _bullets);
                    }

                    foreach (var bullet in _bullets)
                    {
                        bullet.Update(gameTime);
                        // Check collision:
                        if (_player._parryBound.Intersects(bullet.Position))
                        {
                            _player.setParry(true);
                        }
                    }
                    _player.Update();
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