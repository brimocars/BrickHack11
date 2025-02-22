using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11
{
    public class Bullet : GameObject
    {
        public Texture2D SpriteSheet { get; private set; }
        public Rectangle Position { get; private set; }
        public Rectangle SpriteFrame { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; private set; }
        
        public Bullet(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame, Vector2 velocity, Vector2 acceleration)
            : base(spriteSheet, position, spriteFrame)
        {
            SpriteSheet = spriteSheet;
            Position = position;
            SpriteFrame = spriteFrame;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity = new Vector2(Velocity.X + Acceleration.X, Velocity.Y + Acceleration.Y);
            Position = new Rectangle(
                (int)(Position.X + Velocity.X * elapsed),
                (int)(Position.Y + Velocity.Y * elapsed),
                Position.Width,
                Position.Height);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, Color.White);
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
