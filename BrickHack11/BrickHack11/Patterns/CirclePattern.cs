﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11.Patterns;

public class CirclePattern : IBulletPattern
{
    private int _numBullets;
    private float _bulletSpeed;
    public float Cost { get; set; }

    public CirclePattern(int numBullets, float bulletSpeed, float cost)
    {
        _numBullets = numBullets;
        _bulletSpeed = bulletSpeed;
        Cost = cost;
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
                new Vector2((int)origin.X, (int)origin.Y),
                new Rectangle((int)origin.X, (int)origin.Y, 30, 30),
                bulletFrame,
                velocity,
                new Vector2(0, 0));

                bulletList.Add(bullet);
        }
    }
}