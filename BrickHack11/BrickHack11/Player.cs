using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace BrickHack11
{
    class Player : GameObject
    {
        private bool isGodMode = false;
        private bool _isAlive;
        private int _health;
        private float _speed = 3f;
        public float _parryCooldown = 0f;
        private float _cooldownDuration = 1f;
        public Rectangle _parryBound;
        private int currentIFrames = 0;
        private Texture2D _shieldSprite;

        public bool IsAlive { get { return _isAlive; } private set { _isAlive = value; } }
        public bool IsInvulnerable { get { return currentIFrames > 0; } }

        public Player(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame, int health, float speed, Texture2D shieldSprite) :
          base(spriteSheet, position, hitbox, spriteFrame)
        {
            _isAlive = true;
            _health = health;
            _speed = speed;
            _shieldSprite = shieldSprite;

            // Create parry bounds based on player frame:
            _parryBound = new Rectangle((int)Position.X, (int)Position.Y - Hitbox.Height / 2, Hitbox.Width, Hitbox.Height / 2);
        }

        public void Update()
        {
            currentIFrames--;
            if (!_isAlive)
            {
                // game over state
            }
            KeyboardState state = Keyboard.GetState();
            Vector2 newPos = Position;


            if (state.IsKeyDown(Keys.W))
                newPos.Y -= (int)_speed;
            if (state.IsKeyDown(Keys.A))
                newPos.X -= (int)_speed;
            if (state.IsKeyDown(Keys.S))
                newPos.Y += (int)_speed;
            if (state.IsKeyDown(Keys.D))
                newPos.X += (int)_speed;

            Position = newPos;

            // Update parry box:
            _parryBound = new Rectangle((int)Position.X, (int)Position.Y - Hitbox.Height / 2, Hitbox.Width, Hitbox.Height / 2);

            // Update cooldown:
            if (!CanParry()) 
            {
                _parryCooldown -= (1f/60f);
            }
        }

        public void TakeDamage()
        {
            if (isGodMode) return;
            _health--;
            if (_health <= 0)
            {
                _isAlive = false;
            }
            currentIFrames = Constants.iFrames;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Hitbox, SpriteFrame, Color.White);
            spriteBatch.Draw(_shieldSprite, _parryBound, new Rectangle(0, 0, _shieldSprite.Width, _shieldSprite.Height), Color.Orange);
        }

        public bool CanParry()
        {
            return _parryCooldown <= 0;
        }

        internal void ResetCooldown()
        {
            _parryCooldown = _cooldownDuration;
        }

        public void BackToStart(float startX, float startY)
        {
            Vector2 newPos = new Vector2(startX, startY);
            Position = newPos;
        }
    }



}