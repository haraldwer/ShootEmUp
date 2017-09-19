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
    class EnemyBullet
    {
        Texture2D mySprite;
        public Vector2 myPos;
        Vector2 mySpeed;
        int myVelocity; //The speed with which the bullet will move forward with
        public float myDir = 0f;
        public bool myAlive = true;
        public string myHit = "";

        // Constructor
        public EnemyBullet(Texture2D aSprite, Vector2 aPos, float aDir, GeneralMethods aMethod, int aVelocity)
        {
            mySprite = aSprite;
            myPos = aPos;
            myDir = aDir;
            myVelocity = aVelocity;
            mySpeed = aMethod.SpeedFromDir(myDir) * myVelocity;
            myPos += mySpeed;
        }

        // Update-event
        public void Update(GeneralMethods aMethod, Player aPlayer)
        {
            myPos += mySpeed;
            #region Collisionss
                if (aMethod.PointCollision(new Vector2(myPos.X + 16, myPos.Y + 16), 32, aPlayer.myPos, 64))
                {
                    aPlayer.myHP -= 1;
                    myAlive = false;
                }
            #endregion
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos + new Vector2(32, 32) - aViewPos, null, Color.White, myDir + 1.57f, new Vector2(32, 32), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

    }
}
