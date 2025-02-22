using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;

namespace BrickHack11
{
    class Bullet : GameObject
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


        public Bullet(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame, Vector2 velocity, Vector2 acceleration, int screenWidth, int screenHeight)
            : base(spriteSheet, position, spriteFrame)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
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
            // Checks to see if the bullet has slowed to less than 0.5 pixels/frame
            if (true)
                return true;
            return false;
        }
    }
}
