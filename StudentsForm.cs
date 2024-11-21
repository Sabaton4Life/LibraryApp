using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Biblioteca
{
    public partial class StudentsForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Biblioteca\dataBiblioteca\dateBiblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public StudentsForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from StudentTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDB.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void StudentsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (studentId.Text == "" || studentName.Text == "" || studentSpecializare.Text == "" || studentAnStudiu.Text == "" || studentTelefon.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {

                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into StudentTbl values (@studentId,@studentName,@studentSpecializare,@studentAnStudiu,@studentTelefon)", Con);
                cmd.Parameters.AddWithValue("@studentId", int.Parse(studentId.Text));
                cmd.Parameters.AddWithValue("@studentName", studentName.Text);
                cmd.Parameters.AddWithValue("@studentSpecializare", studentSpecializare.Text);
                cmd.Parameters.AddWithValue("@studentAnStudiu", studentAnStudiu.Text);
                cmd.Parameters.AddWithValue("@studentTelefon", studentTelefon.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Added");
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Delete StudentTbl where studentId = @studentId", Con);
            cmd.Parameters.AddWithValue("@studentId", int.Parse(studentId.Text));
            cmd.ExecuteNonQuery();
            Con.Close();
            MessageBox.Show("Student Deleted!");
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (studentId.Text == "" || studentName.Text == "" || studentSpecializare.Text == "" || studentAnStudiu.Text == "" || studentTelefon.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update StudentTbl set studentName=@studentName, studentSpecializare=@studentSpecializare, studentAnStudiu=@studentAnStudiu, studentTelefon=@studentTelefon where studentId=@studentId", Con);
                cmd.Parameters.AddWithValue("@studentId", int.Parse(studentId.Text));
                cmd.Parameters.AddWithValue("@studentName", studentName.Text);
                cmd.Parameters.AddWithValue("@studentSpecializare", studentSpecializare.Text);
                cmd.Parameters.AddWithValue("@studentAnStudiu", studentAnStudiu.Text);
                cmd.Parameters.AddWithValue("@studentTelefon", studentTelefon.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Edited");
                populate();
            }
        }
    }
}
