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
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP6__DS2____Aziz_Belhadj_Sayar
{
    public partial class Employes : Form
    {
        SqlConnection cn = new SqlConnection("Data Source = AZIZPC;Initial Catalog=HR;Integrated Security=True");
        
        public Employes()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Employes_MouseDown);
            this.MouseMove += new MouseEventHandler(Employes_MouseMove);
        }

        private void iconEXIT_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void iconBACK_Click(object sender, EventArgs e)
        {
            Menu obj=new Menu();
            obj.Show();
            this.Hide();
        }

        private void Employes_Load(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("select RTRIM(FIRST_NAME),RTRIM(LAST_NAME) from employees", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string nom = reader.GetString(0);
                string prenom = reader.GetString(1);
                listBoxEmployés.Items.Add(nom +" "+ prenom);
            }
            reader.Close();

            cmd = new SqlCommand("select JOB_ID from JOBS", cn);
            reader= cmd.ExecuteReader();
            while (reader.Read())
            {
                string val=reader.GetString(0);
                cboxJOBID.Items.Add(val);
            }
            reader.Close();

            cmd = new SqlCommand("select DEPARTMENT_ID from DEPARTMENTS", cn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string val2=reader.GetByte(0).ToString();
                cboxDEPID.Items.Add(val2);
            }
            reader.Close();

            cn.Close();
            listBoxEmployés.SelectedIndex = 0;
            dtpEmbauche.Value = System.DateTime.Today;
        }

        private void listBoxEmployés_SelectedIndexChanged(object sender, EventArgs e)
        {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("select * from employees where CONCAT(RTRIM(FIRST_NAME),' ',RTRIM(LAST_NAME))=@CONC", cn);
                    cmd.Parameters.AddWithValue("@CONC", listBoxEmployés.Items[listBoxEmployés.SelectedIndex].ToString());
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtID.Text = reader.GetInt32(0).ToString();
                        txtPrenom.Text = reader.GetString(1).Trim();
                        txtNom.Text = reader.GetString(2).Trim();
                        dtpEmbauche.Text = reader.GetDateTime(3).ToString();
                        cboxJOBID.SelectedItem = reader.GetString(4);
                        cboxDEPID.SelectedItem = reader.GetByte(5).ToString();
                    }
                    reader.Close();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cn.Close();
                }
        }

        private void btnNouveau_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtNom.Text == "" || txtPrenom.Text == "" || cboxDEPID.Text == "" || cboxJOBID.Text == "" || dtpEmbauche.Text == "")
                MessageBox.Show("Veuiller remplir tout les champs","Informations incomplètes",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else
            {
                try
                {

                    cn.Open();
                    SqlCommand cmd = new SqlCommand("insert into EMPLOYEES values (@EMPLOYEE_ID,@FIRST_NAME,@LAST_NAME,@HIRE_DATE,@JOB_ID,@DEPARTMENT_ID)", cn);
                    cmd.Parameters.AddWithValue("@EMPLOYEE_ID", int.Parse(txtID.Text));
                    cmd.Parameters.AddWithValue("@FIRST_NAME", txtPrenom.Text);
                    cmd.Parameters.AddWithValue("@LAST_NAME", txtNom.Text);
                    cmd.Parameters.AddWithValue("@HIRE_DATE", DateTime.Parse(dtpEmbauche.Text));
                    cmd.Parameters.AddWithValue("@JOB_ID", cboxJOBID.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DEPARTMENT_ID", byte.Parse(cboxDEPID.SelectedItem.ToString()));
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Employé ajouté avec succès", "AJOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Employes obj = new Employes();
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
            if (listBoxEmployés.SelectedIndex < 0)
                MessageBox.Show("Veuillez sélectionner un employé de la liste", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("delete employees where EMPLOYEE_ID=@ID", cn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Employé supprimé avec succès", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    cn.Close();
                    Employes obj = new Employes();
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

        private void btnMAJ_Click_1(object sender, EventArgs e)
        {
            if (listBoxEmployés.SelectedIndex < 0)
                    MessageBox.Show("Veuillez sélectionner un employé de la liste", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if((listBoxEmployés.SelectedIndex>=0)&&(txtID.Text==""||txtNom.Text==""||txtPrenom.Text==""||dtpEmbauche.Text==""||cboxJOBID.Text==""||cboxDEPID.Text==""))
                MessageBox.Show("Veuillez remplir tout les champs pour la mise à jour", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("Update employees set FIRST_NAME=@FN,LAST_NAME=@LN,HIRE_DATE=@HD,JOB_ID=@JID,DEPARTMENT_ID=@DID where EMPLOYEE_ID="+txtID.Text, cn);
                    cmd.Parameters.AddWithValue("@FN", txtPrenom.Text);
                    cmd.Parameters.AddWithValue("@LN", txtNom.Text);
                    cmd.Parameters.AddWithValue("@HD", DateTime.Parse(dtpEmbauche.Text));
                    cmd.Parameters.AddWithValue("@JID", cboxJOBID.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DID", byte.Parse(cboxDEPID.SelectedItem.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employé est mis à jour avec succès", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cn.Close();
                    Employes obj = new Employes();
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

        private int current = 0;
        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (current < listBoxEmployés.Items.Count - 1)
            {
                current++;
                listBoxEmployés.SelectedIndex = current;
            }
        }

        private void btnPrécedent_Click(object sender, EventArgs e)
        {
            if (current > 0)
            {
                current--;
                listBoxEmployés.SelectedIndex = current;
            }
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (listBoxEmployés.Items.Count>=1)
            {
                listBoxEmployés.SelectedIndex = 0;
                current = 0;
            }
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (listBoxEmployés.Items.Count >= 1)
            {
                listBoxEmployés.SelectedIndex = listBoxEmployés.Items.Count - 1;
                current = listBoxEmployés.Items.Count - 1;
            }
        }
        private Point _mouseDownLocation;
        private void Employes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = new Point(e.X, e.Y);
            }
        }

        private void Employes_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _mouseDownLocation.X;
                this.Top += e.Y - _mouseDownLocation.Y;
            }
        }

        private void dtpEmbauche_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dtpEmbauche.Value;
            int x = DateTime.Compare(date, System.DateTime.Today);
            if (x > 0)
            {
                MessageBox.Show("Date d'embauche ne peut pas etre supérieure à " + System.DateTime.Today.ToString().Substring(0, 10), "Date d'embauche incorrecte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpEmbauche.Value = System.DateTime.Today;
                this.BringToFront();
            }
        }

        private void txtPrenom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                MessageBox.Show("Des lettres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Des chiffres seulemnt ! ", "Type incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }
    }
}
