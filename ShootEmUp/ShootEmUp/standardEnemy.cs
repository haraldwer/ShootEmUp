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
        Texture2D myBulletSprite;
        Texture2D mySprite;
        public Vector2 myPos;
        Vector2 mySpeed;
        float myDir = 0f;
        Random myRNG = new Random();
        int myBulletTimer = 0;
        int myMagSize;
        int myShotsFired = 0;

        // Constructor
        public StandardEnemy(Texture2D aSprite, Vector2 aPos, float aDir, Texture2D aBulletSprite, int aMagSize)
        {
            mySprite = aSprite;
            myPos = aPos;
            myDir = aDir;
            mySpeed = new Vector2((float)Math.Cos(myDir), (float)Math.Sin(myDir));
            myBulletSprite = aBulletSprite;
            myMagSize = aMagSize;
        }

        // Update-event
        public void Update(Player aPlayer, List<EnvironmentObject> anEnvironmentList, GeneralMethods someGeneralMethods, List<Bullet> aBulletList)
        {
            myBulletTimer++;
            myDir = someGeneralMethods.PointDirection(myPos, aPlayer.myPos); //Update direction to point towards the player
            mySpeed = new Vector2((float)Math.Cos(myDir), (float)Math.Sin(myDir)); //Update the speed
            #region Collisions
            foreach (EnvironmentObject w in anEnvironmentList)
            {
                if (someGeneralMethods.PointCollision(new Vector2(myPos.X + mySpeed.X + 16, myPos.Y + 16), 32, w.myPos, 64)) //If theres a collision on the left or right side (depending on direction)
                {
                    mySpeed.X = 0; //Set the left/right speed to 0;
                }
                if (someGeneralMethods.PointCollision(new Vector2(myPos.X + 16, myPos.Y + mySpeed.Y + 16), 32, w.myPos, 64)) //If theres a collision on the upper or bottom side (depending on direction)
                {
                    mySpeed.Y = 0; //Set the upp/down speed to 0;
                }
            }
            #endregion
            if (someGeneralMethods.PointDistance(myPos, aPlayer.myPos) > 200) //If the player is within a range of 200
            {
                myPos += mySpeed; //Move towards the player
            }

            #region Weapon
            if(myShotsFired >= myMagSize) //Reload Timer
            {
                myBulletTimer = 60*-3; //Set the weapon to cool down for an extra 3 seconds
                myShotsFired = 0; //Reset the number of shots fired
            }
            if(myBulletTimer >= 60*0.75) //Weapon Cooldown
            {
                aBulletList.Add(new Bullet(myBulletSprite, myPos, myDir, someGeneralMethods, 10)); //Create a new bullet
                myBulletTimer = 0; //Reset the weapon cooldown
                myShotsFired++; //Reset the reload timer
            }
            #endregion
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos + new Vector2(32, 32)-aViewPos, null, Color.White, myDir - 1.57f, new Vector2(32, 32), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
