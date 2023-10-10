using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Nouveau_mdp : Form
    {
        public Nouveau_mdp()
        {
            InitializeComponent();
        }

        private void iconBACK_Click(object sender, EventArgs e)
        {
            Se_connecter obj= new Se_connecter();
            obj.Show();
            this.Hide();
        }

        private void iconEXIT_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnConfirmer_Click(object sender, EventArgs e)
        {
            if (this.txtNouveau.Text != "")
            {
                string nouveau = $"{txtNouveau.Text}";
                File.WriteAllText("MDP.txt", nouveau);
                MessageBox.Show("Mot de passe changé avec succès !","CHANGEMENT",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Se_connecter obj = new Se_connecter();
                obj.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Veuillez saisir le nouveau mot de passe", "Information incomplète", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
