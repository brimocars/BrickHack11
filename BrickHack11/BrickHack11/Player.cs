using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace BrickHack11 
{
    class Player : GameObject
    {
      private bool _isAlive;
      private int _health;
      private float _speed = 3f;
      private bool _canParry;
      private Bullet _bulletToParry;
      public Rectangle _parryBound;
        public Player(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame, int health, float speed) : base(spriteSheet, position, spriteFrame)
        {
          _isAlive = true;
          _health = health;
          _speed = speed;
          _canParry = false;

          // Create parry bounds based on player frame:
          _parryBound = new Rectangle(position.X + position.Width, position.Y, position.Width / 2,  position.Height);
        }

        public void Update()
        {
          if (!_isAlive)
          {
            // game over state
          }
          KeyboardState state = Keyboard.GetState();
          Rectangle newPos = Position;


          if(state.IsKeyDown(Keys.W))
            newPos.Y -= (int)_speed;
          if(state.IsKeyDown(Keys.A))
            newPos.X -= (int)_speed;
          if(state.IsKeyDown(Keys.S))
            newPos.Y += (int)_speed;
          if(state.IsKeyDown(Keys.D))
            newPos.X += (int)_speed;

          Position = newPos;

          // Update parry box:
           _parryBound = new Rectangle(Position.X + Position.Width, Position.Y, Position.Width / 2,  Position.Height);

           // Check Parry:
           if(_canParry && state.IsKeyDown(Keys.Space))
           {
            //performParry();
           }
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
          spriteBatch.Draw(SpriteSheet, Position, SpriteFrame, Color.Green);
        }

        public void setParry(bool canParry, Bullet parryBullet)
        {
            _canParry = canParry;
            _bulletToParry = parryBullet;
        }
    }



}