﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickHack11.Patterns;

public interface IBulletPattern
{
    public float Cost { get; set; }
    
    void Spawn(Vector2 origin, Texture2D bulletTexture, Rectangle bulletFrame, List<Bullet> bulletList);
}