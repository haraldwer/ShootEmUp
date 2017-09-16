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
    class GeneralMethods
    {
        public bool PointCollision(Vector2 aPos, int aSize, Vector2 anotherPos, int anotherSize)
        {
            Rectangle tempHitbox = new Rectangle(new Point(Convert.ToInt32(aPos.X), Convert.ToInt32(aPos.Y)), new Point(aSize, aSize));
            Rectangle tempSecondHitbox = new Rectangle(new Point(Convert.ToInt32(anotherPos.X), Convert.ToInt32(anotherPos.Y)), new Point(anotherSize, anotherSize));
            if (!Rectangle.Intersect(tempHitbox, tempSecondHitbox).IsEmpty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
                
        public int PointDistance(Vector2 aPos, Vector2 anotherPos)
        {
            int tempDis = Convert.ToInt32(Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(aPos.X - anotherPos.X), 2) + Math.Pow(Convert.ToDouble(aPos.Y - anotherPos.Y), 2))));
            return tempDis;
        }
    }
}
