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
    }
}
