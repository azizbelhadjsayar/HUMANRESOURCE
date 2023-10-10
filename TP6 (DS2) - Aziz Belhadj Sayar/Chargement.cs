using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Chargement : Form
    {
        public Chargement()
        {
            InitializeComponent();
        }
        int x = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            x += 3;
            PB.Value = x;
            label1.Text = x.ToString() + "%";
            if(PB.Value ==100 ) 
            {
                PB.Value = 0;
                timer1.Stop();
                Menu obj= new Menu();
                obj.Show();
                this.Hide();
            }
        }

        private void Chargement_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
