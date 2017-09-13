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
        Texture2D sprite;
        Vector2 pos;
        Vector2 speed;
        float movementSpeed = 0.7f;
        float maxSpeed = 4f;
        

        // Constructor
        public Player(Texture2D aSprite)
        {
            sprite = aSprite;
            pos = new Vector2(100, 100);
            speed = new Vector2(0, 0);
        }


        // Update-event
        public void Update()
        {
            speed = (speed / 10) * 9; // Friction (makes it slow down)

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                speed.Y -= movementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                speed.Y += movementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                speed.X -= movementSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                speed.X += movementSpeed;
            }

            if (speed.X > maxSpeed)
            {
                speed.X = maxSpeed;
            }
            if (speed.X < -maxSpeed)
            {
                speed.X = -maxSpeed;
            }
            if (speed.Y > maxSpeed)
            {
                speed.Y = maxSpeed;
            }
            if (speed.Y < -maxSpeed)
            {
                speed.Y = -maxSpeed;
            }

            // A poor attempt at making natural movement
            if ((Math.Abs(speed.X) + Math.Abs(speed.Y)) > maxSpeed)
            {
                float diagonalSpeed = (float)Math.Sqrt(Convert.ToDouble(Math.Pow(maxSpeed, 2) / 2));
                if (speed.X > diagonalSpeed){speed.X = diagonalSpeed;}
                if (speed.X < -diagonalSpeed) { speed.X = -diagonalSpeed; }
                if(speed.Y > diagonalSpeed) {speed.Y = diagonalSpeed;}
                if (speed.Y < -diagonalSpeed) { speed.Y = -diagonalSpeed; }
            }

            pos += speed; // Add speed to position
        }

        // Draw-event
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, pos, Color.White);
            spriteBatch.End();
        }

    }
}
