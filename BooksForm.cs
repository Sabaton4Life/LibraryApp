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
    public partial class BooksForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Biblioteca\dataBiblioteca\dateBiblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public BooksForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            BookDB.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void BooksForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numeCarte.Text == "" || Autor.Text == "" || Genre.Text == "" || Editura.Text == "" || Pret.Text == "" || Cantitate.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {

                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BookTbl values (@numeCarte,@Autor,@Genre,@Editura,@Pret,@Cantitate)", Con);
                cmd.Parameters.AddWithValue("@numeCarte", numeCarte.Text);
                cmd.Parameters.AddWithValue("@Autor", Autor.Text);
                cmd.Parameters.AddWithValue("@Genre", Genre.Text);
                cmd.Parameters.AddWithValue("@Editura", Editura.Text);
                cmd.Parameters.AddWithValue("@Pret", Pret.Text);
                cmd.Parameters.AddWithValue("@Cantitate", Cantitate.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Added");
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Delete BookTbl where numeCarte = @numeCarte", Con);
            cmd.Parameters.AddWithValue("@numeCarte", numeCarte.Text);
            cmd.ExecuteNonQuery();
            Con.Close();
            MessageBox.Show("Book Deleted!");
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numeCarte.Text == "" || Autor.Text == "" || Genre.Text == "" || Editura.Text == "" || Pret.Text == "" || Cantitate.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update BookTbl set Autor=@Autor, Genre=@Genre, Editura=@Editura, Pret=@Pret, Cantitate=@Cantitate where numeCarte=@numeCarte", Con);
                cmd.Parameters.AddWithValue("@numeCarte", numeCarte.Text);
                cmd.Parameters.AddWithValue("@Autor", Autor.Text);
                cmd.Parameters.AddWithValue("@Genre", Genre.Text);
                cmd.Parameters.AddWithValue("@Editura", Editura.Text);
                cmd.Parameters.AddWithValue("@Pret", Pret.Text);
                cmd.Parameters.AddWithValue("@Cantitate", Cantitate.Text);
                cmd.ExecuteNonQuery();

                Con.Close();
                MessageBox.Show("Succesfully Edited");
                populate();
            }
        }
    }
}
