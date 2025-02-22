using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11;

public class Enemy : GameObject
{
    private bool _isAlive; // did the player win? am i spawned in?
    private int _health;
    private float _speed;
    private List<Bullet> _bullets; // commented until briguy adds bullets
    private float _attackCooldown; // how long between attacks?
    private float _timeSinceLastAttack; // how long until i can spawn another bullet pattern?
    
    public Enemy(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame, int health, float speed) : 
        base(spriteSheet, position, hitbox, spriteFrame)
    {
        _isAlive = true;
        _health = health;
        _speed = speed;
        _bullets = new List<Bullet>();
        _attackCooldown = 3.0f; // every three seconds spawn a pattern
        _timeSinceLastAttack = 0;
    }
    
    public void Update(GameTime gameTime)
    {
        if (!_isAlive) return;
        
        // Example movement: bro just moves downward
        Position = new Vector2(Position.X, Position.Y + (int)(_speed * gameTime.ElapsedGameTime.TotalSeconds));

        // calculate how long since spawning last pattern
        _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timeSinceLastAttack >= _attackCooldown)
        {
            SpawnPattern();
            _timeSinceLastAttack = 0;
        }

        // Update bullets
        foreach (var bullet in _bullets)
            bullet.Update(gameTime);
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
        if (!_isAlive) // only draw bro if hes alive
        {
            spriteBatch.Draw(SpriteSheet, Position, SpriteFrame, Color.Red);
        }

        foreach (var bullet in _bullets)
        {
            bullet.Draw(spriteBatch);
        }
    }
}