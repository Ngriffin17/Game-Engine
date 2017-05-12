using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormExample
{
    public class Enemy : PhysicsSprite
    {
        private Boolean left = false;
        private Random r = new Random();
        
        public Enemy(int x, int y) : base(Properties.Resources.Boo, x, y)
        {
            Vx = -4f;
            Engine.enemyCount++;
        }

        public void Shoot()
        {
            Bullet bullet = new Bullet((int)(X + Width * Scale * 1.1f), (int)(Y + Height * Scale / 2), 3);
            if (left)
            {
                bullet.X = X - 26;
                bullet.Vx *= -1;
            }
            Engine.canvas.csAdd(bullet);
        }

        public bool isWall()
        {
            X += Vx;
            List<CollisionSprite> list = getCollisions();
            X -= Vx;
            if (list.Count > 0)
            {
                foreach (CollisionSprite s in list)
                    if (s.GetType() == typeof(Elephant))
                        s.Kill();
                return true;
            }
            return false;
        }

        public override void Kill()
        {
            base.Kill();
            Engine.canvas.csRemove(this);
        }

        public class Bullet : PhysicsSprite
        {
            DateTime start;
            TimeSpan lifetime = new TimeSpan(0, 0, 0, 1, 50);

            public Bullet(int x, int y, int type) : base(Properties.Resources.Bullet3, x, y)
            {
                if (type == 1)
                {
                    Vx = 60f;
                    Gy = 0;
                }
                if (type == 2)
                {
                    Vx = -60f;
                    Gy = 0;
                }
                if (type == 3)
                {
                    Vx = (float)Math.Sin(45) * 60;
                    Vy = (float)Math.Cos(45) * 60;
                    Gy = 0;
                }
                if (type == 4)
                {
                    Vx = (float)Math.Sin(90) * 60;
                    Vy = (float)Math.Cos(90) * 60;
                    Gy = 0;
                }
                if (type == 5)
                {
                    Vx = -(float)Math.Sin(-90) * 60;
                    Vy = -(float)Math.Cos(-90) * 60;
                    Gy = 0;
                }
                if (type == 6)
                {
                    Vx = -(float)Math.Sin(90) * 60;
                    Vy = -(float)Math.Cos(90) * 60;
                    Gy = 0;
                }
                setMotion(1);
                start = DateTime.Now;
            }

            public void killCharacter()
            {
                X += Vx;
                List<CollisionSprite> list = getCollisions();
                X -= Vx;
                if (list.Count > 0)
                {
                    foreach (CollisionSprite s in list)
                    {
                        if (s.GetType() == typeof(Elephant))
                        {
                            s.Kill();
                        }
                    }
                    this.Kill();
                }
            }
            
            public override void Kill()
            {
                base.Kill();
                Engine.canvas.csRemove(this);
            }

            public override void act()
            {
                base.act();
                killCharacter();
                if (DateTime.Now - start >= lifetime) Kill();
            }

        }

        public class FriendlyBullet : PhysicsSprite
        {
            DateTime start;
            TimeSpan lifetime = new TimeSpan(0, 0, 0, 1, 50);

            public FriendlyBullet(int x, int y) : base(Properties.Resources.Bullet3, x, y)
            {
                Vx = 10f;
                setMotion(1);
                start = DateTime.Now;
            }

            public void killCharacter()
            {
                X += Vx;
                List<CollisionSprite> list = getCollisions();
                X -= Vx;
                foreach (CollisionSprite s in list)
                {
                    if (s.GetType() == typeof(Enemy))
                    {
                        s.Kill();
                        Engine.enemyCount -= 1;
                    }
                }
                if (list.Count > 0) this.Kill();
            }

            public override void Kill()
            {
                base.Kill();
                Engine.canvas.csRemove(this);
            }

            public override void act()
            {
                base.act();
                killCharacter();
                if (DateTime.Now - start >= lifetime) Kill();
            }

        }

        public override void act()
        {
            base.act();
            if (r.NextDouble() < .01) Vy = -20;
            if (isWall()) Vx *= -1;
            if (r.NextDouble() < .01) Shoot();
            if (Vx < 0) left = true;
            if (Vx > 0) left = false;
        }
    }
}