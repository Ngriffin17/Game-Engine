using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;


namespace FormExample
{
    public partial class Engine : Form
    {
        public static Form form;
        public static Thread thread;
        public static Thread thread2;
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static double s2 = 100;
        static Sprite sp = new Sprite();
        public static SlideSprite elephant;
        public static Image img = Image.FromFile("Rhino.png");


        protected override void OnKeyDown(KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            //Console.WriteLine("asdffasdf");
            if (e.KeyCode == Keys.Left)
                elephant.tx -= 30;
            if (e.KeyCode == Keys.Right)
                elephant.tx += 30;
            if (e.KeyCode == Keys.Up)
                elephant.ty -= 30;
            if (e.KeyCode == Keys.Down)
                elephant.ty += 30;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Engine());
        }

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            elephant = new SlideSprite(img);
            elephant.tx = 100;
            elephant.ty = 100;
            elephant.v = 5;
            sp.add(elephant);
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
                elephant.update();
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
            if (e.KeyChar == 'a') elephant.tx -= 30;
            if (e.KeyChar == 'd') elephant.tx += 30;
            if (e.KeyChar == 'w') elephant.ty -= 30;
            if (e.KeyChar == 's') elephant.ty += 30;
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
