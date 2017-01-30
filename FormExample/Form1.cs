using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;


namespace FormExample
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread thread;
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static double s2 = 100;
        Box sprite = new Box();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(run));
            thread.Start();
        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                Console.WriteLine(running_fps);
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds< frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime-diff).Milliseconds);
                last = DateTime.Now;
                s++;
                s2 += 1 / running_fps;
                form.Invoke(new MethodInvoker(form.Refresh));
                
            }
        }

        private void UpdateSize()
        {
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
                sprite.add(new Sprite());
            if (e.KeyChar == '2')
                for (int i = 0; i < 2; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '3')
                for (int i = 0; i < 3; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '4')
                for (int i = 0; i < 4; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '5')
                for (int i = 0; i < 5; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '6')
                for (int i = 0; i < 6; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '7')
                for (int i = 0; i < 7; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '8')
                for (int i = 0; i < 8; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '9')
                for (int i = 0; i < 9; i++)
                    sprite.add(new Sprite());
            if (e.KeyChar == '0')
                sprite.clear();
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
            String s22 = "FPS: " + running_fps;
            e.Graphics.DrawString(s22, new Font("Comic Sans MS", 10), Brushes.Black, 0, 0);
            sprite.render(e.Graphics);
            int v = (int)(200 + 100 * Math.Sin(s2));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, v, v));
        }
    }
}
