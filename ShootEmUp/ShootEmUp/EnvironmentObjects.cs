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
    class EnvironmentObject
    {
        Texture2D mySprite;
        public Vector2 myPos;
        int myType;

        // Constructor
        public EnvironmentObject(int aType, Vector2 aPos, Texture2D aSprite)
        {
            mySprite = aSprite;
            myPos = aPos * 64;
        }

        // Update-event
        public void Update()
        {

        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Draw(mySprite, myPos-aViewPos, Color.White);
        }

    }
}
