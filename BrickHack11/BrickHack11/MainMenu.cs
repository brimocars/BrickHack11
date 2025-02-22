using BrickHack11;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;


public class MainMenu 
{
        private Texture2D menuBackgroud;
        Rectangle playButton;
        //Rectangle settingButton;
        Rectangle quitButton;

        public bool playClick = false;
        public bool quitClick = false;

        public MainMenu()  
        {

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