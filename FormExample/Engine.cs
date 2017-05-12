using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;


namespace FormExample
{
    public partial class Engine : Form
    {
        public static Elephant elephant;
        public static bool left, right, isRender, resetting, win, lose;
        public static Form form;
        public static Thread thread;
        public static Thread thread2;
        public static int x, y, lastUpdate, enemyCount;
        public static Rectangle rect = new Rectangle(0, 0, 1920, 1080, 200);
        public static TextSprite text = new TextSprite(0, 0, "Hey look, you did it.\nWhat, were you expecting a trophy?\nWell that takes too much work to make, so you get this box instead.\n[]\nNow stop reading and press r to restart.");
        public static TextSprite loss = new TextSprite(0, 0, "Wow, good job nerd.\nI bet you also died on the first level of Mario\nI would've put the entire Bee Movie script here but it wouldn't fit.\nSo just press r to restart.");
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static double s2 = 100;
        public static Sprite canvas = new Sprite();

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(render));
            thread2 = new Thread(new ThreadStart(update));
            thread.Start();
            thread2.Start();
            canvas.add(rect);
            canvas.add(text);
            canvas.add(loss);
        }

        public static void render()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                if (enemyCount <= 0 && !lose)
                {
                    rect.setColor(Rectangle.initColor);
                    rect.setVisibility(true);
                    text.setVisibility(true);
                    text.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
                    win = true;
                }
                else if (!elephant.alive && !win)
                {
                    rect.setColor(Color.FromArgb(200, Color.Red));
                    rect.setVisibility(true);
                    loss.changeLocation((form.ClientSize.Width / 2) - 50, (form.ClientSize.Height / 2) - 50);
                    loss.setVisibility(true);
                    lose = true;
                }
                else
                {
                    rect.setVisibility(false);
                    text.setVisibility(false);
                    loss.setVisibility(false);
                    win = false;
                    lose = false;
                }
                if (lastUpdate > 10) continue;
                isRender = true;
                form.Invoke(new MethodInvoker(form.Refresh));
                isRender = false;
            }
        }

        public static void update()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                if (resetting) continue;
                canvas.update();
                lastUpdate += 1;
                if (!isRender)
                {
                    lastUpdate = 0;
                    canvas.queueClear();
                    canvas.updateAllTracking();
                }
            }
        }

        private void UpdateSize()
        {

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {

            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Down)
            {
                elephant.Vy += 30;
                elephant.Ay += -0.2f;
            }
            else if (e.KeyCode == Keys.Up)
            {
                elephant.Vy -= 20;
                if(elephant.Vy > 40)
                    elephant.Vy = 40;
                elephant.Ay = 0.2f;
            }
            else if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.Space)
            {
                elephant.Shoot();
            }
            else if (e.KeyCode == Keys.R)
            {
                reset();
            }
        }

        public void reset()
        {
            canvas.RemoveAll();
            resetting = true;
            elephant = new Elephant(300, 300);
            elephant.alive = true;
            Enemy enemy = new Enemy(600, 300);
            canvas.csAdd(elephant);
            canvas.csAdd(enemy);
            for (int i = -1; i < 19; i++)
            {
                CollisionSprite walls = new CollisionSprite(Properties.Resources.Wall, i * 100, -100);
                canvas.csAdd(walls);
            }
            for (int i = -1; i < 19; i++)
            {
                CollisionSprite walls = new CollisionSprite(Properties.Resources.Wall, i * 100, 1000);
                canvas.csAdd(walls);
            }
            for (int i = 0; i < 10; i++)
            {
                CollisionSprite walls = new CollisionSprite(Properties.Resources.Wall, -100, i * 100);
                canvas.csAdd(walls);
            }
            for (int i = 0; i < 10; i++)
            {
                CollisionSprite walls = new CollisionSprite(Properties.Resources.Wall, 1820, i * 100);
                canvas.csAdd(walls);
            }
            canvas.add(rect);
            canvas.add(text);
            canvas.add(loss);
            resetting = false;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
            thread2.Abort();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            fixScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            fixScale();
        }

        private void fixScale()
        {
            canvas.Scale = Math.Min(ClientSize.Width, ClientSize.Height) / 1000.0f;
            //more code here
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(right)
            {
                elephant.Vx += .5f;
                if (elephant.Vx > 10)
                    elephant.Vx = 10;
                elephant.Ax = -0.1f;
                PhysicsSprite.lastdir = false;
            }
            if(left)
            {
                elephant.Vx -= .5f;
                if (elephant.Vx < -10)
                    elephant.Vx = -10;
                elephant.Ax = 0.1f;
                PhysicsSprite.lastdir = true;
            }
            if (!elephant.alive)
                canvas.csRemove(elephant);
            canvas.render(e.Graphics);
        }
    }
}
