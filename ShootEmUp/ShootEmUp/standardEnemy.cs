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
    class StandardEnemy
    {
        Texture2D mySprite;
        Vector2 myPos;
        Vector2 mySpeed;
        float myDir = 0f;

        // Constructor
        public StandardEnemy(Texture2D aSprite, Vector2 aPos, float aDir)
        {
            mySprite = aSprite;
            myPos = aPos;
            myDir = aDir;
            //mySpeed = new Vector2((float)Math.Cos(myDir), (float)Math.Sin(myDir)) * 10;
        }

        // Update-event
        public void Update()
        {
            //myPos += mySpeed;
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos + new Vector2(32, 32), null, Color.White, myDir + 1.57f, new Vector2(32, 32), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

    }
}
