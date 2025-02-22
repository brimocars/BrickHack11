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
        Vector2 direction = new Vector2(0, 1); // bullets go straight down
        for (int i = 0; i < _numBullets; i++)
        {
            float offsetX = (i - _numBullets / 2) * _offset;
            Vector2 bulletPosition = new Vector2(origin.X + offsetX, origin.Y);
            Bullet bullet = new Bullet(
                bulletTexture,
                new Rectangle((int)origin.X, (int)origin.Y, 10, 10),
                bulletFrame,
                new Vector2(0,-10),
                new Vector2(0, 0));

            bulletList.Add(bullet);
        }
    }
}