using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormExample
{
    class Program : Engine
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
            elephant = new Elephant(300, 300);
            canvas.csAdd(elephant);
            Enemy enemy = new Enemy(600, 300);
            canvas.csAdd(enemy);
            Application.Run(new Program());
        }
    }
}
