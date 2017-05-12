using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FormExample.Enemy;

namespace FormExample
{
    public class Elephant : PhysicsSprite
    {
        public bool alive;

        public Elephant(int x, int y) : base(Properties.Resources.Bays2, x, y)
        {
            Image = Properties.Resources.Bays2;
            X = x;
            Y = y;
            alive = true;
        }

        public override void act()
        {
            base.act();
            if (PhysicsSprite.lastdir)
            {
                Image = Properties.Resources.Bays1;
            }
            else
            {
                Image = Properties.Resources.Bays2;
            }
            if ((Vx > 0 && Ax > 0) || (Vx < 0 && Ax < 0))
            {
                Vx = 0;
                Ax = 0;
            }
            if ((Vy > 0 && Ay > 0) || (Vy < 0 && Ay < 0))
            {
                Vy = 0;
                Ay = 0;
            }
        }

        public override void Kill()
        {
            base.Kill();
            alive = false;
        }

        public void Shoot()
        {
            if (!alive) return;
            FriendlyBullet bullet = new FriendlyBullet((int)(X + 2 * Width * Scale * 1.1f), (int)(Y + Height * Scale / 2));
            bullet.X = X + 2 * Width * Scale * 1.1f;
            bullet.Y = Y + Height * Scale / 2;
            bullet.Vx = 50f;
            if (PhysicsSprite.lastdir)
            {
                bullet.X = X - 50;
                bullet.Vx *= -1;
            }
            else
            {
                bullet.X = X + Width + 50;
            }
            Engine.canvas.csAdd(bullet);
        }
    }
}