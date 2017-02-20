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
        public static Thread thread2;
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static double s2 = 100;
        static Sprite sp = new Sprite();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            for (int i = 0; i < 500; i++)
            {
                sp.add(new Box(1080, 1920));
                Thread.Sleep(10);
            }
            thread = new Thread(new ThreadStart(render));
            thread2 = new Thread(new ThreadStart(update));
            thread.Start();
            thread2.Start();
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
                if (diff.TotalMilliseconds< frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime-diff).Milliseconds);
                last = DateTime.Now;
                form.Invoke(new MethodInvoker(form.Refresh));
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
                sp.update();
            }
        }

        private void UpdateSize()
        {
            
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
            thread2.Abort();
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
                sp.add(new Box(ClientSize.Height,ClientSize.Width));
            if (e.KeyChar == '2')
                for (int i = 0; i < 2; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '3')
                for (int i = 0; i < 3; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '4')
                for (int i = 0; i < 4; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '5')
                for (int i = 0; i < 5; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '6')
                for (int i = 0; i < 6; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '7')
                for (int i = 0; i < 7; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '8')
                for (int i = 0; i < 8; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '9')
                for (int i = 0; i < 9; i++)
                    sp.add(new Box(ClientSize.Height, ClientSize.Width));
            if (e.KeyChar == '0')
                sp.clear();
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
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, 1920, 1080));
            sp.render(e.Graphics);
        }
    }
}
