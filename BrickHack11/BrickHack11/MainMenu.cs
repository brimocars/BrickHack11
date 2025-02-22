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
        private Texture2D _playButton;
        private Texture2D _exitButton;
        private Vector2 playPosition;
        private Vector2 exitPosition;
        private Rectangle playButton;
        //Rectangle settingButton;
        private Rectangle quitButton;



        public bool playClick = false;
        public bool quitClick = false;

        public MainMenu(Texture2D bgTexture, Texture2D playButton, Texture2D quitButton)  
        {
            _bgTexture = bgTexture;
            _playButton = playButton;
            _exitButton = quitButton;
        }

        public void Draw(SpriteBatch sb){
            playButton.X = _bgTexture.Width / 2 + 200;
            playButton.Y = _bgTexture.Height / 2;
            quitButton.X = _bgTexture.Width / 2 + 200;
            quitButton.Y = _bgTexture.Height / 2 + 200;
            playPosition = new Vector2(playButton.X, playButton.Y);
            exitPosition = new Vector2(quitButton.X, quitButton.Y);
            sb.Draw(_bgTexture, new Rectangle(0,0, Constants.ScreenWidth, Constants.ScreenHeight), Color.White);
            sb.Draw(_playButton, playPosition, Color.White);
            sb.Draw(_exitButton, exitPosition, Color.White);
        }   

        public void Update()
        {
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed){
                Rectangle mouseRectangle = new Rectangle(ms.X, ms.Y, 0,0);
                if(mouseRectangle.Intersects(playButton)){
                    playClick = true;
                }
                else if(mouseRectangle.Intersects(quitButton)){
                    quitClick = true;
                }
            }
        }
        
    }