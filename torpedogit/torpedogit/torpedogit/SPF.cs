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
    public partial class SPF : Form
    {
        torpedogit.Map map1;
        Form1 form;
        public SPF(Map m, Form1 f)
        {
            InitializeComponent();
            map1 = m;
            form = f;
        }

        private void SPF_Load(object sender, EventArgs e)
        {
            
        }

        private void sendButt_Click(object sender, EventArgs e)
        {
            int X = 0;
            int Y = 0;
            int O = 0;
            int L = 0;
            try
            {
                X = Convert.ToInt16(tbX);
                Y = Convert.ToInt16(tbY);
                L = Convert.ToInt16(tbL);
                O = Convert.ToInt16(comboBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Rossz input");
                return;
            }

            int[] asd = new int[4];

            Ship ship = new Ship();

            for (int i = 0; i < L; i++)
            {
                ShipPart sp = new ShipPart(1,2,Status.Alive);
                if (O == 0)
                {
                    sp = new ShipPart(X, Y - i*1, Status.Alive);
                }
                else if (O == 1)
                {
                    sp = new ShipPart(X + i*1, Y, Status.Alive);
                }
                else if (O == 2)
                {
                    sp = new ShipPart(X, Y +i*1, Status.Alive);
                }
                else if (O == 3)
                {
                    sp = new ShipPart(X - i * 1, Y, Status.Alive);
                }
                ship.ShipParts.Add(sp);
            }
            ship.Length = L;
            ship.Status = Status.Alive;
            bool bajvan = false;
            foreach (Ship s in map1.Ships)
            {
                foreach (ShipPart p in s.ShipParts)
                {
                    foreach (ShipPart p2 in ship.ShipParts)
                    {
                        if (p2.X == p.X || p2.Y == p.Y)
                        { bajvan = true; }
                        if (p2.X > map1.Width || p2.Y > map1.Height || p2.X < 0 || p2.Y < 0)
                        { bajvan = true; }
                    }
                }
            }

            if (bajvan) { MessageBox.Show("hajóütközés!!!!!"); return; }
            map1.Ships.Add(ship);
            form.drawMap(form.table2, map1);
            this.Close();
        }
    }
}
