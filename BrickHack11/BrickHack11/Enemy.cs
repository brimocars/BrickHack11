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
        private bool _hasShield;
        private int _health;
        private int _shield;
        private float _speed;
        private int _direction; // 1 = right, -1 = left
        private float _attackCooldown;
        private float _timeSinceLastAttack;
        private List<List<IBulletPattern>> _patternGroups;
        private Queue<IBulletPattern> _patternQueue;
        private float _leftBound;
        private float _rightBound;
        private Rectangle _shieldBox;
        private Rectangle _hitbox;
        private Random _random;
        private Texture2D _bulletSprite;
        private Texture2D _shieldSprite;
        private float _patternGroupCooldown;
        private float _timeSinceLastPatternGroup;

        public Enemy(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, 
            Rectangle spriteFrame, int health, float speed, List<List<IBulletPattern>> patterns, Texture2D bulletSprite, Texture2D shieldSprite) 
            : base(spriteSheet, position, hitbox, spriteFrame)
        {
            _isAlive = true;
            _health = health;
            _speed = speed;
            _direction = 1; // Start moving right
            _attackCooldown = 10f;
            _timeSinceLastAttack = _attackCooldown;
            _patternGroupCooldown = 5f;
            _timeSinceLastPatternGroup = _patternGroupCooldown;
            _leftBound = 60;
            _rightBound = 860;
            _patternGroups = patterns;
            _random = new Random();
            _shield = 3;
            _hasShield = true;
            _shieldBox = new Rectangle(hitbox.X - 10, hitbox.Y - 10, hitbox.Width + 20, hitbox.Height + 20);
            _bulletSprite = bulletSprite;
            _shieldSprite = shieldSprite;
            _patternQueue = new Queue<IBulletPattern>();
        }

        public void Update(GameTime gameTime)
        {
            if (!_isAlive) return;

            // Move left or right
            Position = new Vector2(Position.X + _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            //_shieldBox.X = (int)Position.X;
            //_shieldBox.Y = (int)Position.Y;

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
            _timeSinceLastPatternGroup += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public List<Bullet> Attack(Vector2 playerPos)
        {
            var newBullets = new List<Bullet>();
            if (_isAlive && _timeSinceLastPatternGroup >= _patternGroupCooldown)
            {
                if (_patternQueue.Count == 0)
                {
                    int index = _random.Next(_patternGroups.Count);
                    var newGroup = _patternGroups[index];

                    foreach (var pattern in newGroup)
                    {
                        _patternQueue.Enqueue(pattern);
                    }

                    _timeSinceLastPatternGroup = 0;
                }
            }

            if (_isAlive && (_patternQueue.Count != 0) && (_timeSinceLastAttack >= _attackCooldown))
            {
                var patternToFire = _patternQueue.Dequeue();
                _timeSinceLastAttack = 0;
                
                if (patternToFire is TrackingPattern trackingPattern)
                {
                    trackingPattern.Target = playerPos;
                }
                
                patternToFire.Spawn(Position, _bulletSprite, new Rectangle(0, 0, 10, 10), newBullets);
                _timeSinceLastAttack += patternToFire.Cost;
            }

            return newBullets;
        }

        public void TakeDamage()
        {
            if (!_hasShield)
            {
                _health--;
                if (_health <= 0)
                {
                    _isAlive = false;
                }

                newShield();
            }
        }

        public void DamageShield()
        {
            _shield--;
            if (_shield <= 0)
            {
                _hasShield = false;
                _shieldBox = new Rectangle(0, 0, 0, 0);
            }
        }

        private void newShield()
        {
            _hasShield = true;
            _shield = 3;
            _shieldBox = new Rectangle(_hitbox.X - 10, _hitbox.Y - 10, _hitbox.Width + 20, _hitbox.Height + 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isAlive) return;
            spriteBatch.Draw(_shieldSprite, _shieldBox, new Rectangle(0,0,_shieldSprite.Width, _shieldSprite.Height), Color.Azure);

            spriteBatch.Draw(SpriteSheet, Position, SpriteFrame, Color.White);
        }
    }
}
