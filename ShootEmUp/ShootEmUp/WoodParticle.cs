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
    class WoodParticle
    {
        Texture2D mySprite;
        public Vector2 myPos;
        float myDir;
        float myImageAngle;
        Vector2 mySpeed;
        float mySpd;
        public bool myAlive = true;
        int myTimer = 200;
        float myAlpha = 1;

        // Constructor
        public WoodParticle(Vector2 aPos, Texture2D aSprite, float aDir, GeneralMethods aMethod, Random aRnd)
        {
            mySprite = aSprite;
            myPos = aPos;
            myDir = aDir;
            mySpd = aRnd.Next(10);
            mySpeed = aMethod.SpeedFromDir(myDir) * mySpd;
            myImageAngle = aRnd.Next(628) / 10;
        }

        // Update-event
        public void Update()
        {
            mySpeed = mySpeed * 0.9f;
            myPos += mySpeed;

            if (myTimer < 0)
            {
                myAlpha -= 0.01f;
                if (myAlpha < 0)
                {
                    myAlive = false;
                }
            }
            else
            {
                myTimer--;
            }
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Draw(mySprite, myPos - aViewPos, null, new Color(new Vector4(myAlpha, myAlpha, myAlpha, myAlpha)), myImageAngle, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0f);
        }
    }
}
