using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11
{
    public class Bullet : GameObject
    {
        private Vector2 velocity;
        private Vector2 acceleration;
        private int screenHeight;
        private int screenWidth;
        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
        }


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
            if (Position.X > screenWidth || Position.X < 0 || Position.Y > screenHeight || Position.Y < 0)
            {
                // bullet is out of bounds
                return true;
            }
            return false;
        }
    }
}
