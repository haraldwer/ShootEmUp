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
    class Bullet
    {
        Texture2D sprite;
        Vector2 pos;
        Vector2 speed;
        float movementSpeed = 0.7f;
        float dir = 0f;

        // Constructor
        public Bullet(Texture2D aSprite, Vector2 aPos, float aDir)
        {
            sprite = aSprite;
            pos = aPos;
            dir = aDir;
        }

        // Update-event
        public void Update()
        {
            pos += speed;
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
