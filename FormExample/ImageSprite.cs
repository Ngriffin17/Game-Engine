using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    public class ImageSprite : Sprite
    {
        public int x, y, w, h;
        public Image img;

        public Image Image
        {
            get { return img; }
            set { img = value; }
        }

        public ImageSprite(Image img, int x, int y)
        {
            this.img = img;
            X = x;
            Y = y;
            Width = img.Width;
            Height = img.Height;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(img, 0, 0);
        }
    }
}
