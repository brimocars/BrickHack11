using BrickHack11;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;


public class MainMenu 
{
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle playButton;
        //Rectangle settingButton;
        private Rectangle quitButton;
        private SpriteBatch spriteBatch;


        public bool playClick = false;
        public bool quitClick = false;

        public MainMenu(Texture2D texture)  
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch sb){
           sb.Draw(_texture, new Rectangle(0,0, Constants.ScreenWidth, Constants.ScreenHeight), Color.White);
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