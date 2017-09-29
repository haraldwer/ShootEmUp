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
    class HealthPack
    {
        Vector2 myPos;
        Texture2D mySprite;
        public bool myAlive = true;
        bool myPickedUp = false;

        // Constructor
        public HealthPack(Vector2 aPos, Texture2D aSprite)
        {
            myPos = aPos;
            mySprite = aSprite;
        }

        public void Update(Player aPlayer, GeneralMethods aMethod)
        {
            if (aMethod.PointCollision(myPos, 64, aPlayer.myPos, 64))
            {
                myPickedUp = true;
            }
            if (myPickedUp)
            {
                myPos += aMethod.SpeedFromDir(aMethod.PointDirection(myPos, aPlayer.myPos)) * aMethod.PointDistance(myPos, aPlayer.myPos) * 0.1f;
                if (aMethod.PointDistance(myPos, aPlayer.myPos) < 10)
                {
                    aPlayer.myHP += 20;
                    if (aPlayer.myHP > aPlayer.myMaxHp)
                    {
                        aPlayer.myHP = aPlayer.myMaxHp;
                    }
                    myAlive = false;
                }
            }
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Draw(mySprite, myPos - aViewPos, Color.White);
        }
    }
}
