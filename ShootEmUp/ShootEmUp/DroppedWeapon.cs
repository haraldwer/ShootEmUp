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
    class DroppedWeapon
    {
        Vector2 myPos;
        Texture2D mySprite;
        public bool myAlive = true;
        string myWeapon = "";
        float myDir = 0;
        bool myPickedUp = false;

        // Constructor
        public DroppedWeapon(Vector2 aPos, Texture2D aSprite, string aWeapon)
        {
            myPos = aPos;
            mySprite = aSprite;
            myWeapon = aWeapon;
        }

        public void Update(Player aPlayer, GeneralMethods aMethod)
        {
            if (aMethod.PointCollision(myPos, 64, aPlayer.myPos, 64))
            {
                myPickedUp = true;
            }
            if(myPickedUp)
            {
                myPos += aMethod.SpeedFromDir(aMethod.PointDirection(myPos, aPlayer.myPos)) * aMethod.PointDistance(myPos, aPlayer.myPos) * 0.1f;
                if(aMethod.PointDistance(myPos, aPlayer.myPos) < 10)
                {
                    aPlayer.myWeapon = myWeapon;
                    myAlive = false;
                }
            }
        }
        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Draw(mySprite, myPos + new Vector2(32, 32) - aViewPos, null, Color.White, myDir + 1.57f, new Vector2(32, 32), 1.0f, SpriteEffects.None, 0f);
        }
    }
}
