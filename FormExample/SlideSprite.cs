using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FormExample
{
    public class SlideSprite : Sprite
    {
        public int tx, ty, v, x, y;
        private Image img;

        public SlideSprite(Image img)
        {
            this.img = img;
            x = 0;
            y = 0;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(img, x, y);
        }

        public override void act()
        {
            if(x + v < tx)
                x += v;
            else if(x - v > tx)
                x -= v;
            else if(Math.Abs(x - tx) <= v)
                x = tx;
            if (y + v < ty)
                y += v;
            else if (y - v > ty)
                y -= v;
            else if (Math.Abs(y - ty) <= v)
                y = ty;
            if (x == tx && y == ty)
                v = 5;
            else
                v += 5;
            base.act();
        }
    }
}
