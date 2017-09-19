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
    class Player
    {
        Texture2D mySprite;
        public Vector2 myPos;
        Vector2 mySpeed;
        float myMovementSpeed = 0.7f;
        float myMaxSpeed = 4f;
        int myBulletTimer = 0;
        float myDir = 0;
        public int myHP = 10;

        // Constructor
        public Player(Texture2D aSprite)
        {
            mySprite = aSprite;
            myPos = new Vector2(0, 0);
            mySpeed = new Vector2(0, 0);
        }


        // Update-event
        public void Update(GeneralMethods aMethod, List<EnvironmentObject> anEnviromentList, List<Bullet> aBulletList, Texture2D aBulletSprite, MouseState aMouse, Vector2 aViewPos)
        {
            mySpeed = (mySpeed / 10) * 9; // Friction (makes it slow down)

            #region Controls and movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //mySpeed.Y -= myMovementSpeed;
                mySpeed.Y -= ((Math.Pow(mySpeed.X, 2) + Math.Pow(mySpeed.Y - myMovementSpeed, 2)) > Math.Pow(myMaxSpeed, 2) ? 0 : myMovementSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //mySpeed.Y += myMovementSpeed;
                mySpeed.Y += ((Math.Pow(mySpeed.X, 2) + Math.Pow(mySpeed.Y + myMovementSpeed, 2)) > Math.Pow(myMaxSpeed, 2) ? 0 : myMovementSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //mySpeed.X -= myMovementSpeed;
                mySpeed.X -= ((Math.Pow(mySpeed.X - myMovementSpeed, 2) + Math.Pow(mySpeed.Y, 2)) > Math.Pow(myMaxSpeed, 2) ? 0 : myMovementSpeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //mySpeed.X += myMovementSpeed;
                mySpeed.X += ((Math.Pow(mySpeed.X + myMovementSpeed, 2) + Math.Pow(mySpeed.Y, 2)) > Math.Pow(myMaxSpeed, 2) ? 0 : myMovementSpeed);
            }
            #endregion

            #region Direction
            myDir = (float)Math.Atan2(aMouse.Y+aViewPos.Y-myPos.Y, aMouse.X+aViewPos.X-myPos.X); // Calculate what direction player is facing (this is used for example when drawing)
            if (myDir == 0 && mySpeed == new Vector2(0, 0))
            {
                myDir = -1.57f;
            }
            #endregion

            #region Shooting
            if (myBulletTimer > 0) // If bulletTimer is above 0
            {
                myBulletTimer -= 1; // Count down
            }
            else if (aMouse.LeftButton == ButtonState.Pressed) // If key is pressed and bulletTimer is below 0
            {
                aBulletList.Add(new Bullet(aBulletSprite, myPos, myDir, aMethod, 15)); // Add bullet to bulletList
                myBulletTimer = 10; // Reset bulletTimer
            }
            #endregion

            #region Collisions
            foreach(EnvironmentObject w in anEnviromentList)
            {
                if (aMethod.PointCollision(new Vector2(myPos.X + mySpeed.X + 16, myPos.Y + 16), 32, w.myPos, 64))
                {
                    mySpeed.X = 0;
                }
                if (aMethod.PointCollision(new Vector2(myPos.X + 16, myPos.Y + mySpeed.Y + 16), 32, w.myPos, 64))
                {
                    mySpeed.Y = 0;
                }
            }
            #endregion

            if(myHP <= 0)
            {
                Environment.Exit(0);
            }
            myPos += mySpeed; // Add speed to position
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch, Vector2 aViewPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mySprite, myPos + new Vector2(32, 32) - aViewPos, null, Color.White, myDir - 1.57f, new Vector2(32, 32), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

    }
}
