using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11.Patterns;

public class CirclePattern : IBulletPattern
{
    private int _numBullets;
    private float _bulletSpeed;

    public CirclePattern(int numBullets, float bulletSpeed)
    {
        _numBullets = numBullets;
        _bulletSpeed = bulletSpeed;
    }

    public void Spawn(Vector2 origin, Texture2D bulletTexture, Rectangle bulletFrame, List<Bullet> bulletList)
    {
        float angleStep = MathHelper.TwoPi / _numBullets;
        for (int i = 0; i < _numBullets; i++)
        {
            float angle = i * angleStep;
            Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * _bulletSpeed;
            Bullet bullet = new Bullet(
                bulletTexture,
                new Rectangle((int)origin.X, (int)origin.Y, 10, 10),
                bulletFrame,
                velocity,
                new Vector2(0, 0));

                bulletList.Add(bullet);
        }
    }
}