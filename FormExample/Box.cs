using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    public class Box : Sprite
    {
        private int x, y, x2, y2, h, w, r;

        public Box(int h, int w):base()
        {
            this.h = h;
            this.w = w;
            Random rand = new Random();
            r = rand.Next(1, 1000);
            x2 = rand.Next(1, 1920);
            x = x2;
            y2 = rand.Next(1, 1080);
            y = y2;
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(Brushes.PowderBlue, new Rectangle(x, y, 20, 20));
        }

        public override void act()
        {
            x = (x + 10) % (w - 1);
            y = (y + 8) % (h - 1);
            base.act();
        }
    }
}
