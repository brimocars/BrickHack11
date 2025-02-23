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
        HitStop,
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
        private Enemy _enemy;
        private int hitStopTimer;

        private List<Bullet> _enemyBullets;
        private List<Bullet> _parriedBullets;
        private Vector2 startingPosition;

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
            _enemyBullets = new List<Bullet>();
            _parriedBullets = new List<Bullet>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new SpriteManager(this.Content);

            startingPosition = new Vector2(Constants.ScreenWidth/2, Constants.ScreenHeight - (Constants.ScreenHeight / 8));

            _player = new Player(
                sprites.PlayerSprite,
                new Vector2(startingPosition.X, startingPosition.Y),
                new Rectangle((int)startingPosition.X, (int)startingPosition.Y, 64, 100),
                new Rectangle(0, 0, sprites.PlayerSprite.Width, sprites.PlayerSprite.Height),
                3, 6.8f,
                sprites.ShieldSprite);

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
                    if (mainMenu.playClick)
                    {
                        _gameState = GameState.Playing;
                    }
                    else if (mainMenu.quitClick)
                    {
                        Exit();
                    }
                    _previousGameState = GameState.MainMenu;
                    break;

                case GameState.Playing:
                    if (_previousGameState == GameState.MainMenu)
                    {
                        var patternGroups = new List<List<IBulletPattern>>
                        {   new List<IBulletPattern>
                            {
                                new TrackingPattern(1, 650, 9.8f),
                                new CirclePattern(30, 300, 9f),
                            }
                        };

                        _enemy = new Enemy(
                            sprites.EnemySprite,
                            new Vector2(480, 100),
                            new Rectangle(300, 300, sprites.EnemySprite.Width - 30, sprites.EnemySprite.Height - 30),
                            new Rectangle(0, 0, sprites.EnemySprite.Width, sprites.EnemySprite.Height),
                            3,
                            200,
                            patternGroups,
                            sprites.BulletSprite
                            );
                    }

                    // Update Enemy
                    _enemy.Update(gameTime);

                    // Update Player
                    _player.Update();

                    // Update Bullets
                    for (int i = 0; i < _enemyBullets.Count; i++)
                    {
                        Bullet bullet = _enemyBullets[i];
                        bullet.Update(gameTime);

                        // Check collision:
                        if (!_player.IsInvulnerable && _player.Hitbox.Intersects(bullet.Hitbox))
                        {
                            _enemyBullets.RemoveAt(i);
                            _gameState = GameState.HitStop;
                            hitStopTimer = 0;
                            i--;
                            _player.TakeDamage();
                        }
                    }

                    if (_enemy.Hitbox.Intersects(_player.Hitbox) && !_player.IsInvulnerable)
                    {
                        _gameState = GameState.HitStop;
                        hitStopTimer = 0;

                        _player.TakeDamage();
                    }

                    // Handle new bullets from enemy
                    List<Bullet> newBullets = _enemy.Attack(_player.Position);
                    _enemyBullets.AddRange(newBullets);

                    // Check for parry:
                    KeyboardState state = Keyboard.GetState();
                    if (state.IsKeyDown(Keys.Space))
                    {
                        // Check parry for bullets:
                        for (int i = 0; i < _enemyBullets.Count; i++)
                        {

                            // Check collision:
                            if (_player._parryBound.Intersects(_enemyBullets[i].Hitbox) && _player.canParry())
                            {
                                _gameState = GameState.HitStop;
                                hitStopTimer = 0;
                                Bullet removedBullet = _enemyBullets[i];
                                _enemyBullets.RemoveAt(i);
                                i--;
                                removedBullet.IsParried = true;
                                removedBullet.enemy = _enemy;
                                _parriedBullets.Add(removedBullet);
                            }
                        }
                        // Reset parry:
                        _player.resetCooldown();

                        // Check for a melee attack:
                        if (_player._parryBound.Intersects(_enemy.Hitbox))
                        {
                            // Run a melee animation?
                            _gameState = GameState.HitStop;
                            hitStopTimer = 0;
                            _enemy.TakeDamage();
                            _player.BackToStart(startingPosition.X, startingPosition.Y);
                        }
                    }

                    for (int i = 0; i < _parriedBullets.Count; i++)
                    {
                        _parriedBullets[i].Update(gameTime);
                        if (_parriedBullets[i].Hitbox.Intersects(_enemy.Hitbox))
                        {
                            _parriedBullets.RemoveAt(i);
                            i--;
                            _enemy.DamageShield();
                        }
                    }


                    if (!_player.IsAlive)
                    {
                        _gameState = GameState.GameOver;
                    }
                    _previousGameState = GameState.Playing;
                    break;
                case GameState.HitStop:
                    hitStopTimer++;
                    if (hitStopTimer >= Constants.hitStopDelay)
                    {
                        _gameState = GameState.Playing;
                    }
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
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                null, null, null, null);

            switch (_gameState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(_spriteBatch);
                    break;

                case GameState.Playing:
                    _player?.Draw(_spriteBatch);
                    _enemy?.Draw(_spriteBatch);

                    foreach (Bullet bullet in _enemyBullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }

                    foreach (Bullet bullet in _parriedBullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    break;
                case GameState.HitStop:
                    _player?.Draw(_spriteBatch);
                    _enemy?.Draw(_spriteBatch);

                    foreach (Bullet bullet in _enemyBullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    foreach (Bullet bullet in _parriedBullets)
                    {
                        bullet.Draw(_spriteBatch);
                    }
                    break;
                case GameState.Paused:
                    break;

                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.Red);
                    break;

                case GameState.Victory:
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
