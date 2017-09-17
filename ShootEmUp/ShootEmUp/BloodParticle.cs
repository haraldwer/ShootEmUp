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
    class BloodParticle
    {
        Texture2D mySprite;
        public Vector2 myPos;
        float myDir;
        Vector2 mySpeed;
        float mySpd;
        public bool myAlive = true;

        // Constructor
        public BloodParticle(Vector2 aPos, Texture2D aSprite, float aDir, GeneralMethods aMethod, Random aRnd)
        {
            mySprite = aSprite;
            myPos = aPos;
            myDir = aDir;
            mySpd = aRnd.Next(10);
            mySpeed = aMethod.SpeedFromDir(myDir) * mySpd;
        }

        // Update-event
        public void Update()
        {
            mySpeed = mySpeed * 0.9f;
            myPos += mySpeed;
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos - aViewPos, null, Color.White, myDir + 1.57f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
