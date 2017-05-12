using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    public class PhysicsSprite : CollisionSprite
    {
        enum Motion { Static, SetPath, Kinematic };

        Motion Motions = Motion.Kinematic;

        public static bool lastdir;
        float gx = 0, gy = 1f, ax = 0, ay = 0, vx = 0, vy = 0;

        public float Gx { get { return gx; } set { gx = value; } }
        public float Gy { get { return gy; } set { gy = value; } }
        public float Ax { get { return ax; } set { ax = value; } }
        public float Ay { get { return ay; } set { ay = value; } }
        public float Vx { get { return vx; } set { vx = value; } }
        public float Vy { get { return vy; } set { vy = value; } }

        public PhysicsSprite(Image image, int x, int y) : base(image,x,y)
        {
            X = x;
            Y = y;
        }

        public void setMotion(int type)
        {
            if (type == 0) Motions = Motion.Static;
            if (type == 1) Motions = Motion.SetPath;
            if (type == 2) Motions = Motion.Kinematic;
        }

        public override void act()
        {
            if (Motions != Motion.Static)
            {
                X += Vx;
                if (getCollisions().Count > 0)
                {
                    X -= Vx;
                    Vx = 0;
                }
                Y += Vy;
                if (getCollisions().Count > 0)
                {
                    Y -= Vy;
                    Vy = 0;
                }
            }
            if (Motions == Motion.Kinematic)
            {
                Vx += Ax + Gx;
                Vy += Ay + Gy;
            }
            base.act();
        }
    }
}
