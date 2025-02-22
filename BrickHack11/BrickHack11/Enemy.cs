using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11;

public class Enemy : GameObject
{
    private bool _isAlive;
    private int _health;
    private float _speed;
    private int _direction; // 1 = right, -1 = left
    private float _attackCooldown;
    private float _timeSinceLastAttack;
    private List<Bullet> _bullets;

    private float _leftBound;
    private float _rightBound;

    public Enemy(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame, int health, float speed) 
        : base(spriteSheet, position, hitbox, spriteFrame)
    {
        _isAlive = true;
        _health = health;
        _speed = speed;
        _direction = 1; // Start moving right
        _bullets = new List<Bullet>();
        _attackCooldown = 3.0f;
        _timeSinceLastAttack = 0;

        _leftBound = 100;
        _rightBound = 900;
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

        // Attack logic
        _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeSinceLastAttack >= _attackCooldown)
        {
            SpawnPattern();
            _timeSinceLastAttack = 0;
        }

        // Update bullets
        foreach (var bullet in _bullets)
        {
            bullet.Update(gameTime);
        }
    }

    private void SpawnPattern()
    {
        return;
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

        foreach (var bullet in _bullets)
        {
            bullet.Draw(spriteBatch);
        }
    }
}
