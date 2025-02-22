using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickHack11 
{
    class PlayerObject : GameObject
    {
      private float speed = 3f;
        public PlayerObject(Texture2D spriteSheet, Rectangle position, Rectangle spriteFrame) : base(spriteSheet, position, spriteFrame)
        {
        }

        public void Update()
        {
          KeyboardState state = Keyboard.GetState();
          Rectangle newPos = Position;

          if(state.IsKeyDown(Keys.W))
            newPos.Y -= (int)speed;
          if(state.IsKeyDown(Keys.A))
            newPos.X -= (int)speed;
          if(state.IsKeyDown(Keys.S))
            newPos.Y += (int)speed;
          if(state.IsKeyDown(Keys.D))
            newPos.X += (int)speed;

          Position = newPos;
        }
    }



}