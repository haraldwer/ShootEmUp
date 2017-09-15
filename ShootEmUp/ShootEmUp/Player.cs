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

        // Constructor
        public Player(Texture2D aSprite)
        {
            mySprite = aSprite;
            myPos = new Vector2(100, 100);
            mySpeed = new Vector2(0, 0);
        }


        // Update-event
        public void Update(List<Bullet> aBulletList, Texture2D aBulletSprite, Vector2 aViewPos, Vector2 aMousePosition)
        {
            mySpeed = (mySpeed / 10) * 9; // Friction (makes it slow down)

            #region Controls
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                mySpeed.Y -= myMovementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                mySpeed.Y += myMovementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                mySpeed.X -= myMovementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                mySpeed.X += myMovementSpeed;
            }
            #endregion

            #region Limiting speed
            if (mySpeed.X > myMaxSpeed)
            {
                mySpeed.X = myMaxSpeed;
            }
            if (mySpeed.X < -myMaxSpeed)
            {
                mySpeed.X = -myMaxSpeed;
            }
            if (mySpeed.Y > myMaxSpeed)
            {
                mySpeed.Y = myMaxSpeed;
            }
            if (mySpeed.Y < -myMaxSpeed)
            {
                mySpeed.Y = -myMaxSpeed;
            }
            #endregion

            #region Diagonal movement
            // A poor attempt at making natural movement
            float tempDiagonalSpeed = (float)Math.Sqrt(Convert.ToDouble(Math.Pow(myMaxSpeed, 2) / 2));
            if ((Math.Abs(mySpeed.X) + Math.Abs(mySpeed.Y)) > tempDiagonalSpeed)
            {
                if (mySpeed.X > tempDiagonalSpeed){mySpeed.X = tempDiagonalSpeed;}
                if (mySpeed.X < -tempDiagonalSpeed) { mySpeed.X = -tempDiagonalSpeed; }
                if(mySpeed.Y > tempDiagonalSpeed) {mySpeed.Y = tempDiagonalSpeed;}
                if (mySpeed.Y < -tempDiagonalSpeed) { mySpeed.Y = -tempDiagonalSpeed; }
            }
            #endregion

            #region Direction
            myDir = (float)Math.Atan2(aMousePosition.Y+aViewPos.Y-myPos.Y, aMousePosition.X+aViewPos.X-myPos.X); // Calculate what direction player is facing (this is used for example when drawing)
            if (myDir == 0 && mySpeed == new Vector2(0, 0))
            {
                myDir = -1.57f;
            }
            #endregion

            myPos += mySpeed; // Add speed to position

            #region Shooting
            if (myBulletTimer > 0) // If bulletTimer is above 0
            {
                myBulletTimer -= 1; // Count down
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space)) // If key is pressed and bulletTimer is below 0
            {
                aBulletList.Add(new Bullet(aBulletSprite, myPos, myDir)); // Add bullet to bulletList
                myBulletTimer = 10; // Reset bulletTimer
            }
            #endregion
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
