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
        public int tx, ty, w, h;
        private int v = 5;
        public Image img;
        private Image image;

        public SlideSprite(Image img, int x2, int y2)
        {
            this.img = img;
            X = x2;
            Y = y2;
            tx = x2;
            ty = y2;
        }

        public SlideSprite(Image image)
        {
            this.image = image;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(img, 0, 0);
        }
        
        public override void act()
        {
            if(X + v < tx)
                X += v;
            else if(X - v > tx)
                X -= v;
            else if(Math.Abs(X - tx) <= v)
                X = tx;
            if (Y + v < ty)
                Y += v;
            else if (Y - v > ty)
                Y -= v;
            else if (Math.Abs(Y - ty) <= v)
                Y = ty;
            if (X == tx && Y == ty)
                v = 5;
            else
                v += 5;
            base.act();
        }
    }
}
