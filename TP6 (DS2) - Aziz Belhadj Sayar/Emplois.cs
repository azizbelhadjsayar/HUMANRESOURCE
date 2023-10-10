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
    public partial class Emplois : Form
    {
        SqlConnection cn = new SqlConnection("Data Source = AZIZPC;Initial Catalog=HR;Integrated Security=True");
        public Emplois()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.MouseDown += new MouseEventHandler(Emplois_MouseDown);
            this.MouseMove += new MouseEventHandler(Emplois_MouseMove);
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

        private void Emplois_Load(object sender, EventArgs e)
        {
            remplissage("select * from JOBS");
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtID.Text = row.Cells["JOB_ID"].Value.ToString().Trim();
                txtTitre.Text = row.Cells["JOB_TITLE"].Value.ToString().Trim();
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
            if (dataGridView1.Rows.Count >= 1)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                dataGridView1.Rows[0].Selected = true;
                current = 0;
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1)
            {
                int dernier = dataGridView1.RowCount - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = dernier;
                dataGridView1.Rows[dernier].Selected = true;
                current = dataGridView1.Rows.Count - 1;
            }
        }

        private void txtFiltre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                remplissage("SELECT * FROM JOBS WHERE JOB_ID LIKE '" + txtFiltre.Text + "%'" + " or  JOB_TITLE LIKE '" + txtFiltre.Text + "%'");
                if (dataGridView1.Rows.Count == 0)
                {
                    txtID.Text = "";
                    txtTitre.Text = "";
                    dataGridView1.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtTitre.Text == "")
                MessageBox.Show("Veuiller remplir tout les champs", "Informations incomplètes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into JOBS values (@JOB_ID,@JOB_TITLE)", cn);
                    cmd.Parameters.AddWithValue("@JOB_ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@JOB_TITLE", txtTitre.Text);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Emploi ajouté avec succès", "AJOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Emplois obj = new Emplois();
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
                SqlCommand cmd = new SqlCommand("delete JOBS where JOB_ID=@ID", cn);
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Emploi supprimé avec succès", "SUPPRESSION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.Close();
                Emplois obj = new Emplois();
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
            if (txtID.Text == "" || txtTitre.Text == "")
                MessageBox.Show("Veuillez remplir tout les champs", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update JOBS set JOB_ID=@JID,JOB_TITLE=@JT where JOB_ID LIKE '" + dataGridView1.SelectedCells[0].Value.ToString() + "'", cn);
                    cmd.Parameters.AddWithValue("@JID", txtID.Text);
                    cmd.Parameters.AddWithValue("@JT", txtTitre.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Emploi est mis à jour avec succès", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    Emplois obj = new Emplois();
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

        private Point _mouseDownLocation;
        private void Emplois_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = new Point(e.X, e.Y);
            }
        }

        private void Emplois_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _mouseDownLocation.X;
                this.Top += e.Y - _mouseDownLocation.Y;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtTitre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }
    }
}
