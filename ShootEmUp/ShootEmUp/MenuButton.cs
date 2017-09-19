using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    class MenuButton
    {
        Texture2D mySprite;
        Vector2 myPos;
        string myText;
        public int myOption;
        public bool myClicked = false;
        bool myClickBegin = false;

        // Constructor
        public MenuButton(string aText, int anOption, Texture2D aSprite, Vector2 aPos, int aButtonWidth)
        {
            myOption = anOption;
            myText = aText;
            mySprite = aSprite;
            myPos = new Vector2(aPos.X, aPos.Y + aButtonWidth * anOption);
        }

        // Update-event
        public void Update(MouseState aMouseState, GeneralMethods aMethod)
        {
            myClicked = false;
            if (!myClickBegin)
            {
                if (aMethod.PointCollision(myPos, 64, new Vector2(aMouseState.X + 32, aMouseState.Y + 32), 1))
                {
                    if (aMouseState.LeftButton == ButtonState.Pressed)
                    {
                        myClickBegin = true;
                    }
                }
            }
            else
            {
                if (aMouseState.LeftButton == ButtonState.Released)
                {
                    myClickBegin = false;
                    if (aMethod.PointCollision(myPos, 64, new Vector2(aMouseState.X + 32, aMouseState.Y + 32), 1))
                    {
                        myClicked = true;
                    }
                }
            }
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos, Color.White); 
            spriteBatch.End();
        }
    }
}
