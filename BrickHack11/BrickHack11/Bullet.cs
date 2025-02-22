using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11
{
    public class Bullet : GameObject
    {
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; private set; }
        
        public Bullet(Texture2D spriteSheet, Vector2 position, Rectangle hitbox, Rectangle spriteFrame, Vector2 velocity, Vector2 acceleration)
            : base(spriteSheet, position, hitbox, spriteFrame)
        {
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity = new Vector2(Velocity.X + Acceleration.X, Velocity.Y + Acceleration.Y);
            Position = new Vector2((Position.X + Velocity.X * elapsed), (Position.Y + Velocity.Y * elapsed));
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Hitbox, Color.White);
        }

        // public bool Move()
        // {
        //     velocity = new Vector2(velocity.X + acceleration.X, velocity.Y + acceleration.Y);
        //     Position = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y), Position.Width, Position.Height);
        //     // Check if bullet is off screen
        //     if (Position.X > Constants.ScreenWidth || Position.X < 0 || Position.Y > Constants.ScreenHeight || Position.Y < 0)
        //     {
        //         // bullet is out of bounds
        //         return true;
        //     }
        //     return false;
        // }
    }
}
