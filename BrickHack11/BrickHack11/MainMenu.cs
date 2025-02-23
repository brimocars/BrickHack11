//using System.Numerics;
using BrickHack11;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;


public class MainMenu
{
    private Texture2D _bgTexture;
    private Texture2D _playButtonTexture;
    private Texture2D _quitButtonTexture;
    private Rectangle playButton;
    //Rectangle settingButton;
    private Rectangle quitButton;



    public bool playClick = false;
    public bool quitClick = false;
    public bool mouseClick = false;

    public MainMenu(Texture2D bgTexture, Texture2D playButton, Texture2D quitButton)
    {
        _bgTexture = bgTexture;
        _playButtonTexture = playButton;
        _quitButtonTexture = quitButton;
        this.playButton = new Rectangle(bgTexture.Width / 2 + 200, bgTexture.Height / 2 + 80, _playButtonTexture.Width, _playButtonTexture.Height);
        this.quitButton = new Rectangle(bgTexture.Width / 2 + 200, bgTexture.Height / 2 + 250, _quitButtonTexture.Width, _quitButtonTexture.Height);
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(_bgTexture, new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight), Color.White);
        sb.Draw(_playButtonTexture, playButton, Color.White );
        sb.Draw(_quitButtonTexture, quitButton, Color.White);
    }

    public void Update()
    {
        MouseState ms = Mouse.GetState();
        if (ms.LeftButton == ButtonState.Pressed && mouseClick == false)
        {
            mouseClick = true;
            Rectangle mouseRectangle = new Rectangle(ms.X, ms.Y, 1, 1);
            if (mouseRectangle.Intersects(playButton))
            {
                playClick = true;
            }
            else if (mouseRectangle.Intersects(quitButton))
            {
                quitClick = true;
            }
            else
            {
                mouseClick = false;
            }
        }
    }

}