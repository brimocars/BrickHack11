using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11
{
    public class Bullet : GameObject
    {
        private Vector2 velocity;
        private Vector2 acceleration;

        public bool Enabled { get; }

        public Bullet(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame, Vector2 velocity, Vector2 acceleration)
            : base(spriteSheet, position, spriteFrame)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        public void Update(List<Bullet> projectileList)
        {
            if (Move())
            {
                projectileList.Remove(this);
                return;
            }
        }

        public bool Move()
        {
            velocity = new Vector2(velocity.X + acceleration.X, velocity.Y + acceleration.Y);
            Position = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y), Position.Width, Position.Height);
            // Check if bullet is off screen
            if (Position.X > Constants.ScreenWidth || Position.X < 0 || Position.Y > Constants.ScreenHeight || Position.Y < 0)
            {
                // bullet is out of bounds
                return true;
            }
            return false;
        }
    }
}
