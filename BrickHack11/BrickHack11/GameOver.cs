using BrickHack11;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;

public class GameOver
{
    private Texture2D _bgTexture;

    public GameOver(Texture2D bgTexture)
    {
        _bgTexture = bgTexture;
    }

     public void Draw(SpriteBatch sb)
    {
        sb.Draw(_bgTexture, new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight), Color.White);
    }

}