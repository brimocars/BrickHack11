using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11.Patterns
{
    public class TrackingPattern : IBulletPattern
    {
        private int _numBullets;
        private float _bulletSpeed;

        // Cost is used by the enemy to adjust its cooldown after firing this pattern.
        public float Cost { get; set; }

        // The target position that the bullets will be aimed at.
        public Vector2 Target { get; set; }

        public TrackingPattern(int numBullets, float bulletSpeed, float cost)
        {
            _numBullets = numBullets;
            _bulletSpeed = bulletSpeed;
            Cost = cost;
            Target = Vector2.Zero;
        }

        public void Spawn(Vector2 origin, Texture2D bulletTexture, Rectangle bulletFrame, List<Bullet> bulletList)
        {
            // Calculate the normalized direction from the origin to the target.
            Vector2 direction = Vector2.Normalize(Target - origin);

            // Spawn a stream of bullets along that direction.
            for (int i = 0; i < _numBullets; i++)
            {
                // Each bullet is offset along the firing line.
                Vector2 bulletPos = origin;
                Bullet bullet = new Bullet(
                    bulletTexture,
                    bulletPos,
                    new Rectangle((int)bulletPos.X, (int)bulletPos.Y, 25, 25), // Adjust bullet size as needed.
                    bulletFrame,
                    direction * _bulletSpeed,
                    Vector2.Zero);
                bulletList.Add(bullet);
            }
        }
    }
}