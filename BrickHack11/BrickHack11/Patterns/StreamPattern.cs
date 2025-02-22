using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BrickHack11.Patterns;

public class StreamPattern : IBulletPattern
{
    private int _numBullets;
    private float _bulletSpeed;
    private float _offset;

    public StreamPattern(int numBullets, float bulletSpeed, float offset)
    {
        _numBullets = numBullets;
        _bulletSpeed = bulletSpeed;
        _offset = offset;
    }

    public void Spawn(Vector2 origin, Texture2D bulletTexture, Rectangle bulletFrame, List<Bullet> bulletList)
    {
        // Bullets go straight down, using the bulletSpeed parameter.
        Vector2 velocity = new Vector2(0, _bulletSpeed);
        
        for (int i = 0; i < _numBullets; i++)
        {
            float offsetX = (i - _numBullets / 2) * _offset;
            // Use the calculated bulletPosition instead of the origin.
            Vector2 bulletPosition = new Vector2(origin.X + offsetX, origin.Y);
            
            Bullet bullet = new Bullet(
                bulletTexture,
                bulletPosition,
                new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y, 10, 10),
                bulletFrame,
                velocity,
                new Vector2(0, 0)
            );

            bulletList.Add(bullet);
        }
    }
}
