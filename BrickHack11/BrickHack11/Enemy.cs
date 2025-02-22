using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BrickHack11.Patterns;

namespace BrickHack11
{
    public class Enemy : GameObject
    {
        private bool _isAlive;
        private int _health;
        private float _speed;
        private int _direction; // 1 = right, -1 = left
        private float _attackCooldown;
        private float _timeSinceLastAttack;
        private List<IBulletPattern> _patterns;
        private float _leftBound;
        private float _rightBound;
        private Random _random;

        public Enemy(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, 
            Rectangle spriteFrame, int health, float speed, List<IBulletPattern> patterns) 
            : base(spriteSheet, position, hitbox, spriteFrame)
        {
            _isAlive = true;
            _health = health;
            _speed = speed;
            _direction = 1; // Start moving right
            _attackCooldown = 0f;
            _timeSinceLastAttack = 0;
            _leftBound = 60;
            _rightBound = 860;
            _patterns = patterns;
            _random = new Random();
        }

        public void Update(GameTime gameTime)
        {
            if (!_isAlive) return;

            // Move left or right
            Position = new Vector2(Position.X + _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);

            // Reverse direction at bounds
            if (Position.X <= _leftBound)
            {
                Position = new Vector2(_leftBound, Position.Y);
                _direction = 1; // Move right
            }
            else if (Position.X >= _rightBound)
            {
                Position = new Vector2(_rightBound, Position.Y);
                _direction = -1; // Move left
            }

            // Update attack cooldown
            _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public List<Bullet> SpawnBullets()
        {
            List<Bullet> newBullets = new List<Bullet>();

            if (_isAlive && _timeSinceLastAttack >= _attackCooldown)
            {
                if (_patterns.Count > 0)
                {
                    int index = _random.Next(_patterns.Count);
                    _patterns[index].Spawn(Position, SpriteSheet, new Rectangle(0, 0, 10, 10), newBullets);
                }

                _timeSinceLastAttack = 0;
            }

            return newBullets;
        }

        public void TakeDamage()
        {
            _health--;
            if (_health <= 0)
            {
                _isAlive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isAlive) return;

            spriteBatch.Draw(SpriteSheet, Position, SpriteFrame, Color.White);
        }
    }
}
