using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Emplacements : Form
    {
        SqlConnection cn = new SqlConnection("Data Source = AZIZPC;Initial Catalog=HR;Integrated Security=True");
        public Emplacements()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.MouseDown += new MouseEventHandler(Emplacements_MouseDown);
            this.MouseMove += new MouseEventHandler(Emplacements_MouseMove);
        }
        private void remplissage(string commande)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(commande, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void iconBACK_Click(object sender, EventArgs e)
        {
            Menu obj = new Menu();
            obj.Show();
            this.Hide();
        }

        private void iconEXIT_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Emplacements_Load(object sender, EventArgs e)
        {
            remplissage("select * from LOCATIONS");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtID.Text = row.Cells["LOCATION_ID"].Value.ToString().Trim();
                txtDet.Text = row.Cells["LOCATION_DETAILS"].Value.ToString().Trim();
                DataGridViewRow ligne = dataGridView1.SelectedRows[0];
                string ville = ligne.Cells["LOCATION_DETAILS"].Value.ToString().Trim();
                if (ville == "Moscou")
                {
                    pictureBox2.Image = Properties.Resources.b;
                }
                else if (ville == "Berlin")
                {
                    pictureBox2.Image = Properties.Resources.a;
                }
                else if (ville == "Zurich")
                {
                    pictureBox2.Image = Properties.Resources.c;
                }
                else if ((ville == "Arizone")||(ville == "New York") || (ville == "New York")|| (ville == "Los Angeles")|| (ville == "San Francisco"))
                {
                    pictureBox2.Image = Properties.Resources.d;
                }
                else if (ville == "Rome")
                {
                    pictureBox2.Image = Properties.Resources.e;
                }
                else if ((ville == "Québec")|| (ville == "Toronto"))
                {
                    pictureBox2.Image = Properties.Resources.f;
                }
                else if (ville == "Manchester")
                {
                    pictureBox2.Image = Properties.Resources.g;
                }
                else
                    pictureBox2.Image= Properties.Resources.clear;
            }
        }

        private int current = 0;

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (current < dataGridView1.Rows.Count - 1)
            {
                current++;
                dataGridView1.Rows[current].Selected = true;
            }
        }

        private void btnPrécedent_Click(object sender, EventArgs e)
        {
            if (current > 0)
            {
                current--;
                dataGridView1.Rows[current].Selected = true;
            }
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>=1)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                dataGridView1.Rows[0].Selected = true;
                current = 0;
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>=1)
            {
                int dernier = dataGridView1.RowCount - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = dernier;
                dataGridView1.Rows[dernier].Selected = true;
                current = dataGridView1.Rows.Count - 1;
            }
        }

        private void btnNouveau_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtDet.Text == "")
                MessageBox.Show("Veuiller remplir tout les champs", "Informations incomplètes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into LOCATIONS values (@LID,@LD)", cn);
                    cmd.Parameters.AddWithValue("@LID", int.Parse(txtID.Text));
                    cmd.Parameters.AddWithValue("@LD", txtDet.Text);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Emplacement ajouté avec succès", "AJOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Emplacements obj = new Emplacements();
                    obj.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cn.Close();
                }
            }
        }

        private void btnSupprimer_Click_1(object sender, EventArgs e)
        {

            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("delete LOCATIONS where LOCATION_ID=@ID", cn);
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Emplacement supprimé avec succès", "SUPPRESSION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
                Emplacements obj = new Emplacements();
                obj.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void btnMAJ_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtDet.Text == "")
                MessageBox.Show("Veuillez remplir tout les champs", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update LOCATIONS set LOCATION_ID=@LID,LOCATION_DETAILS=@LD where LOCATION_ID = " + dataGridView1.SelectedCells[0].Value, cn);
                    cmd.Parameters.AddWithValue("@LID", int.Parse(txtID.Text));
                    cmd.Parameters.AddWithValue("@LD", txtDet.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Emplacement est mis à jour avec succès", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    Emplacements obj = new Emplacements();
                    obj.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cn.Close();
                }
            }
        }

        private void txtFiltre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                remplissage("SELECT * FROM LOCATIONS WHERE CAST(LOCATION_ID AS varchar) LIKE '" + txtFiltre.Text + "%'" + " or  LOCATION_DETAILS LIKE '" + txtFiltre.Text + "%'");
                if (dataGridView1.Rows.Count == 0)
                {
                    txtDet.Text = "";
                    txtID.Text = "";
                    dataGridView1.CurrentCell = null;
                    pictureBox2.Image = Properties.Resources.clear;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }
        private Point _mouseDownLocation;
        private void Emplacements_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = new Point(e.X, e.Y);
            }
        }

        private void Emplacements_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _mouseDownLocation.X;
                this.Top += e.Y - _mouseDownLocation.Y;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Des chiffres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtDet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

    }
}
