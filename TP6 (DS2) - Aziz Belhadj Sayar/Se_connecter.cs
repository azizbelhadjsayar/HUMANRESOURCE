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
    public partial class Se_connecter : Form
    {
        public Se_connecter()
        {
            InitializeComponent();
        }

        private void iconEXIT_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        private void btnConnecter_Click(object sender, EventArgs e)
        {
            string fichier = "MDP.txt";
            string mdp = File.ReadAllText(fichier);
            if ((txtUtilisateur.Text != "") && (txtMDP.Text != ""))
            {
                if (mdp != txtMDP.Text)
                {
                    MessageBox.Show("Mot de passe incorrect ! ", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMDP.Clear();
                    txtMDP.Select();
                }
                else
                {
                    Chargement obj = new Chargement();
                    obj.Show();
                    this.Hide();
                }
            }
            else
                MessageBox.Show("Veuiller remplir tous les champs ! ", "Informations incomplètes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnMDPoublié_Click(object sender, EventArgs e)
        {
            Nouveau_mdp obj=new Nouveau_mdp();
            obj.Show();
            this.Hide();
        }
    }
}
