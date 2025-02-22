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

    public void Spawn(Vector2 origin, Texture2D bulletTexture, List<Bullet> bulletList)
    {
        float angleStep = MathHelper.TwoPi / _numBullets;
    }
}