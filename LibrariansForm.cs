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
    public partial class LibrariansForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Biblioteca\dataBiblioteca\dateBiblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public LibrariansForm()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        public void populate()
        {
            Con.Open();
            string query = "select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrariansDB.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LibrariansForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (libId.Text == "" || libName.Text == "" || libPassword.Text == "" || libPhone.Text == ""  )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into LibrarianTbl values (@libId,@libName,@libPassword,@libPhone)",Con);
                cmd.Parameters.AddWithValue("@libId",int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@libName", libName.Text);
                cmd.Parameters.AddWithValue("@libPassword", libPassword.Text);
                cmd.Parameters.AddWithValue("@libPhone", libPhone.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Added");
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Delete LibrarianTbl where libId = @libId", Con);
            cmd.Parameters.AddWithValue("@libId", int.Parse(textBox1.Text));
            cmd.ExecuteNonQuery();
            Con.Close ();
            MessageBox.Show("Librarian Deleted!");
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (libId.Text == "" || libName.Text == "" || libPassword.Text == "" || libPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update LibrarianTbl set libName=@libName, libPassword=@libPassword, libPhone=@libPhone where libId=@libId", Con);
                cmd.Parameters.AddWithValue("@libId", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@libName", libName.Text);
                cmd.Parameters.AddWithValue("@libPassword", libPassword.Text);
                cmd.Parameters.AddWithValue("@libPhone", libPhone.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Edited");
                populate();
            }
        }
    }
}
