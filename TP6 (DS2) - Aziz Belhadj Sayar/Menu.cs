using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Menu_MouseDown);
            this.MouseMove += new MouseEventHandler(Menu_MouseMove);
        }

        private void iconEXIT_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnEmployes_Click(object sender, EventArgs e)
        {
            Employes obj= new Employes();
            obj.Show();
            this.Hide();
        }

        private void btnEmplois_Click(object sender, EventArgs e)
        {
            Emplois obj= new Emplois();
            obj.Show();
            this.Hide();
        }

        private void btnDepartements_Click(object sender, EventArgs e)
        {
            Departements obj= new Departements();
            obj.Show();
            this.Hide();
        }

        private void btnEmplacements_Click(object sender, EventArgs e)
        {
            Emplacements obj= new Emplacements();
            obj.Show();
            this.Hide();
        }
        private Point _mouseDownLocation;

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = new Point(e.X, e.Y);
            }
        }

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _mouseDownLocation.X;
                this.Top += e.Y - _mouseDownLocation.Y;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Employes obj = new Employes();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Emplois obj = new Emplois();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Departements obj = new Departements();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Emplacements obj = new Emplacements();
            obj.Show();
            this.Hide();
        }
    }
}
