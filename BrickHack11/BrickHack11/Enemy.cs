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
        private List<List<IBulletPattern>> _patternGroups;
        private Queue<IBulletPattern> _patternQueue;
        private float _leftBound;
        private float _rightBound;
        private Random _random;

        public Enemy(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, 
            Rectangle spriteFrame, int health, float speed, List<List<IBulletPattern>> patterns) 
            : base(spriteSheet, position, hitbox, spriteFrame)
        {
            _isAlive = true;
            _health = health;
            _speed = speed;
            _direction = 1; // Start moving right
            _attackCooldown = 10f;
            _timeSinceLastAttack = _attackCooldown;
            _leftBound = 60;
            _rightBound = 860;
            _patternGroups = patterns;
            _random = new Random();
            _patternQueue = new Queue<IBulletPattern>();
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

        public List<Bullet> Attack(Vector2 playerPos)
        {
            var newBullets = new List<Bullet>();
            if (_isAlive && _timeSinceLastAttack >= _attackCooldown)
            {
                if (_patternQueue.Count == 0)
                {
                    int index = _random.Next(_patternGroups.Count);
                    var newGroup = _patternGroups[index];

                    foreach (var pattern in newGroup)
                    {
                        _patternQueue.Enqueue(pattern);
                    }
                }
                
                _timeSinceLastAttack = 0;
                var patternToFire = _patternQueue.Dequeue();
                
                if (patternToFire is TrackingPattern trackingPattern)
                {
                    trackingPattern.Target = playerPos;
                }
                
                patternToFire.Spawn(Position, SpriteSheet, new Rectangle(0, 0, 10, 10), newBullets);
                _timeSinceLastAttack += patternToFire.Cost;
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
