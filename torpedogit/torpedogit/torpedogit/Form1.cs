using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace torpedogit
{
    public partial class Form1 : Form
    {
        //EZ AKAR LENNI AZ AI!!!!
        static int[] target(int[,] table, int n)
        {
            int[] coo = new int[2];
            List<int[]> z = new List<int[]>();
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (table[i, k] == 0)
                    {
                        coo[0] = i;
                        coo[1] = k;
                        z.Add(coo);
                    }
                }
            }

            Random x = new Random();
            return z[x.Next(0, z.Count)].ToArray();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int n = new Int16();

        Map map1;
        Map map2;
        private void newGButt_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int[] shipNumbers = new int[4];
            try
            {
                n = Convert.ToInt16(tbN.Text);
                shipNumbers[0] = Convert.ToInt16(ship2.Text);
                shipNumbers[1] = Convert.ToInt16(ship3.Text);
                shipNumbers[2] = Convert.ToInt16(ship4.Text);
                shipNumbers[3] = Convert.ToInt16(ship5.Text);

                sum = 0;
                foreach (int item in shipNumbers)
                {
                    sum += item;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Rossz input");
            }

            if (n < 8 || n > 10 || sum != 5)
            { MessageBox.Show("8 és 10 közötti számot adjál meg! A hajóid számának összege 5 kell, hogy legyen");
                return; }

            map1 = new Map(n, n);
            map2 = new Map(n, n);
            drawMap(table1, map1);
            drawMap(table2, map2);

            int i = 0;
                torpedogit.SPF spd = new SPF(map1, this);
                spd.Text = "Place the " + i.ToString() + ". ship.";
                spd.Show();
                spd.FormClosed += new System.Windows.Forms.FormClosedEventHandler(asd);


        }
        private void asd(object sender, FormClosingEventArgs e)
        {

        }
       
        public void sad()
        {
        }
        public void drawMap(PictureBox pb, Map map)
        {
            Bitmap image = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                //for (int i = 0; i < map.Width; i++)
                //  {
                //  for (int j = 0; j < map.Height; j++)
                //    {

                // Rectangle gridrect = new Rectangle((pb.Width/map.Width)*i, pb.Height / map.Height * j, pb.Width / map.Width, pb.Height / map.Height);
                //g.DrawRectangle(new Pen(Color.Black, 1), gridrect);
                //     }
                //   }
                for (int i = 0; i <= map.Width; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 2), (float)pb.Width / map.Width * i, 0, (float)pb.Width / map.Width * i, pb.Height);
                }
                for (int j = 0; j <= map.Width; j++)
                {
                    g.DrawLine(new Pen(Color.Black, 2), 0, (float)pb.Height / map.Height * j, pb.Width, (float)pb.Height / map.Height * j);
                }

                foreach (Ship s in map1.Ships)
                {
                    foreach (ShipPart sp in s.ShipParts)
                    {
                        RectangleF rect = new RectangleF((float)sp.X * pb.Width / map.Width, (float)sp.Y * pb.Height / map.Height, (float)1 * pb.Width / map.Width, (float)1 * pb.Height / map.Height);
                        g.FillRectangle(new SolidBrush(Color.CornflowerBlue), rect);
                    }
                }

                //g.FillRectangle(new SolidBrush(Color.Red), finish);
                pb.Image = image;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
    public enum Status { Sank, Alive };
    public class ShipPart
    {
        public int X;
        public int Y;
        public Status Status = Status.Alive;
        public ShipPart(int x, int y, Status s)
        {
            X = x;
            Y = y;
            Status = s;
        }

    }
    public class Ship
    {
        public int Length;
        public List<ShipPart> ShipParts = new List<ShipPart>();
        public Status Status = Status.Alive;
    }
    public class Map
    {
        public int Width;
        public int Height;
        public List<Ship> Ships = new List<Ship>();
        public Map(int w, int h)
        {
            Width = w;
            Height = h;
        }


    }

}
