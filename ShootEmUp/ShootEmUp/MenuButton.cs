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
        SpriteFont myFont;
        Vector2 myPos;
        string myText;
        public int myOption;
        public bool myClicked = false;
        bool myClickBegin = false;
        GeneralMethods myGeneralMethods;

        // Constructor
        public MenuButton(SpriteFont aFont, string aText, int anOption, Texture2D aSprite, Vector2 aPos, int aButtonWidth, GeneralMethods aMethod)
        {
            myGeneralMethods = aMethod;
            myFont = aFont;
            myOption = anOption;
            myText = aText;
            mySprite = aSprite;
            myPos = new Vector2(aPos.X, aPos.Y + aButtonWidth * anOption);
        }

        // Update-event
        public void Update(MouseState aMouseState)
        {
            myClicked = false;
            if (!myClickBegin)
            {
                if (myGeneralMethods.PointCollision(myPos, 128, new Vector2(aMouseState.X + 32, aMouseState.Y + 32), 1))
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
                    if (myGeneralMethods.PointCollision(myPos, 128, new Vector2(aMouseState.X + 32, aMouseState.Y + 32), 1))
                    {
                        myClicked = true;
                    }
                }
            }
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mySprite, myPos, Color.White);
            spriteBatch.DrawString(myFont, myText, myPos + new Vector2(32, 48), Color.Black);
        }
    }
}
