using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Departements : Form
    {
        SqlConnection cn = new SqlConnection("Data Source = AZIZPC;Initial Catalog=HR;Integrated Security=True");

        public Departements()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.MouseDown += new MouseEventHandler(Departements_MouseDown);
            this.MouseMove += new MouseEventHandler(Departements_MouseMove);
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

        private void Departements_Load(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select LOCATION_ID from LOCATIONS", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string val = reader.GetInt32(0).ToString();
                cboxLOCID.Items.Add(val);
            }
            reader.Close();
            cn.Close();
            remplissage("select * from departments");
        }

        private void btnNouveau_Click_1(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("insert into DEPARTMENTS values (@DEP_ID,@DEP_NAME,@LOC_ID)", cn);
                cmd.Parameters.AddWithValue("@DEP_ID", int.Parse(txtDEPID.Text));
                cmd.Parameters.AddWithValue("@DEP_NAME", txtDEPNAME.Text);
                cmd.Parameters.AddWithValue("@LOC_ID", cboxLOCID.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Département ajouté avec succès", "AJOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Departements obj = new Departements();
                obj.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close() ;
            }
        }


        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("delete departments where DEPARTMENT_ID=@VAL", cn);
                cmd.Parameters.AddWithValue("@VAL", dataGridView1.SelectedCells[0].Value.ToString());
                SqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Département supprimé avec succès", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reader.Close();
                cn.Close();
                Departements obj = new Departements();
                obj.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close() ;
            }
        }


        private void txtFiltre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                remplissage("SELECT * FROM DEPARTMENTS WHERE DEPARTMENT_NAME LIKE '" + txtFiltre.Text + "%'" + " or CAST(DEPARTMENT_ID AS varchar) LIKE '" + txtFiltre.Text + "%'");
                if(dataGridView1.Rows.Count ==0) 
                {
                    txtDEPID.Text = "";
                    txtDEPNAME.Text = "";
                    cboxLOCID.SelectedItem = null;
                    dataGridView1.CurrentCell=null;
                    emp.Text = "";
                    label9.Text = "";
                    guna2GroupBox3.CustomBorderColor = Color.MistyRose;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtDEPID.Text = row.Cells["DEPARTMENT_ID"].Value.ToString().Trim();
                txtDEPNAME.Text = row.Cells["DEPARTMENT_NAME"].Value.ToString().Trim();
                cboxLOCID.SelectedItem = row.Cells["LOCATION_ID"].Value.ToString().Trim();
                cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand("select LOCATION_DETAILS from LOCATIONS where LOCATION_ID=" + cboxLOCID.Text, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string val = reader.GetString(0);
                    label9.Text = "Situé à : ";
                    emp.Text = val;
                    guna2GroupBox3.CustomBorderColor = Color.LightGreen;
                }
                reader.Close();
                cn.Close();
            }
        }


        private int current = 0;

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

        private void btnMAJ_Click(object sender, EventArgs e)
        {
            if (txtDEPID.Text == "" || txtDEPNAME.Text == "" || cboxLOCID.Text == "")
                MessageBox.Show("Veuillez remplir tout les champs", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update DEPARTMENTS set DEPARTMENT_ID=@DID,DEPARTMENT_NAME=@DN,LOCATION_ID=@LID where DEPARTMENT_ID = " + dataGridView1.SelectedCells[0].Value, cn);
                    cmd.Parameters.AddWithValue("@DID", byte.Parse(txtDEPID.Text));
                    cmd.Parameters.AddWithValue("@DN", txtDEPNAME.Text);
                    cmd.Parameters.AddWithValue("@LID", int.Parse(cboxLOCID.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Département est mis à jour avec succès", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    Departements obj = new Departements();
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
        private void Departements_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = new Point(e.X, e.Y);
            }
        }

        private void Departements_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _mouseDownLocation.X;
                this.Top += e.Y - _mouseDownLocation.Y;
            }
        }

        private void txtDEPID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Des chiffres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtDEPNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }
    }
}
