using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    public class CollisionSprite : ImageSprite
    {
        public List<CollisionSprite> sprites = new List<CollisionSprite>();
        protected List<CollisionSprite> toTrack = new List<CollisionSprite>();
        protected List<CollisionSprite> toUntrack = new List<CollisionSprite>();

        int mask = 1;
        public int Mask
        {
            get { return mask; }
            set { mask = value; }
        }

        public void track(CollisionSprite s)
        {
            toTrack.Add(s);
        }

        public void untrack(CollisionSprite s)
        {
            toUntrack.Add(s);
        }

        public CollisionSprite(Image image, int x, int y) : base(image, x, y)
        {
            sprites.Add(this);
            X = x;
            Y = y;
        }

        public void updateTracking()
        {
            foreach (CollisionSprite s in toUntrack)
            {
                sprites.Remove(s);
            }
            toUntrack = new List<CollisionSprite>();
            foreach (CollisionSprite s in toTrack)
            {
                sprites.Add(s);
            }
            toTrack = new List<CollisionSprite>();
        }

        public List<CollisionSprite> getCollisions()
        {
            double cx = X + Width / 2;
            double cy = Y + Height / 2;
            double rx = Width / 2;
            double ry = Height / 2;

            List<CollisionSprite> output = new List<CollisionSprite>();
            foreach (CollisionSprite sprite in sprites)
            {
                if (sprite == this) continue;
                if ((mask & sprite.mask) == 0) continue;
                double r2x = sprite.Width / 2;
                double r2y = sprite.Height / 2;
                double c2x = sprite.X + r2x;
                double c2y = sprite.Y + r2y;
                if (Math.Abs(cx - c2x) < r2x + rx)
                    if (Math.Abs(cy - c2y) < r2y + ry)
                        output.Add(sprite);
            }
            return output;
        }
    }
}