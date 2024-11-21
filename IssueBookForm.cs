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
    public partial class IssueBookForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Biblioteca\dataBiblioteca\dateBiblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public IssueBookForm()
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

        private void FillStudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select studentID from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("studentID", typeof(int));
            dt.Load(rdr);
            StdID.ValueMember = "studentID";
            StdID.DataSource = dt;
            Con.Close();
        }
        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select numeCarte from BookTbl where cantitate > "+0+"", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("numeCarte", typeof(string));
            dt.Load(rdr);
            BookID.ValueMember = "numeCarte";
            BookID.DataSource = dt;
            Con.Close();
        }
        private void fetchstddata()
        {
            Con.Open();
            string query = "select * from StudentTbl where studentID =" + StdID.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StdNume.Text = dr["studentName"].ToString();
                StdSpecializare.Text = dr["studentSpecializare"].ToString();
                StdAnStudiu.Text = dr["studentAnStudiu"].ToString();
                StdTelefon.Text = dr["studentTelefon"].ToString();
            }
            Con.Close();
        }
        private void IssueBookForm_Load(object sender, EventArgs e)
        {
            FillStudent();
            FillBook();
            populate();
        }

        private void StdID_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void StdID_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }

        public void populate()
        {
            Con.Open();
            string query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookGrid.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (IssueNUM.Text == "" || StdID.Text == "" || StdSpecializare.Text == "" || StdAnStudiu.Text == "" || StdTelefon.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    //string IssueDate = IssueData.Value.Day.ToString() + "/" + IssueData.Value.Month.ToString() + "/" + IssueData.Value.Year.ToString();
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into IssueTbl values (@IssueNum,@studentId,@studentName,@studentSpecializare,@studentAnStudiu,@studentTelefon,@bookIssued,@IssueDate)", Con);
                    cmd.Parameters.AddWithValue("IssueNum", IssueNUM.Text);
                    cmd.Parameters.AddWithValue("@studentID", StdID.Text);
                    cmd.Parameters.AddWithValue("@studentName", StdNume.Text);
                    cmd.Parameters.AddWithValue("@studentSpecializare", StdSpecializare.Text);
                    cmd.Parameters.AddWithValue("@studentAnStudiu", StdAnStudiu.Text);
                    cmd.Parameters.AddWithValue("@studentTelefon", StdTelefon.Text);
                    cmd.Parameters.AddWithValue("@bookIssued", BookID.Text);
                    cmd.Parameters.AddWithValue("@IssueDate", IssueData.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Book Succesfully Issued");
                    UpdateBook();
                    populate();
                }
            }
        }

        private void UpdateBook()
        {
            int cantitate, new_cantitate;
            Con.Open();
            string query = "select * from BookTbl where numeCarte ='" + BookID.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cantitate = Convert.ToInt32(dr["Cantitate"].ToString());
                new_cantitate = cantitate - 1;
                string query1 = "update BookTbl set Cantitate = " + new_cantitate + " where numeCarte = '" + BookID.SelectedValue.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }
        private void UpdateBookRemove()
        {
            int cantitate, new_cantitate;
            Con.Open();
            string query = "select * from BookTbl where numeCarte ='" + BookID.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cantitate = Convert.ToInt32(dr["Cantitate"].ToString());
                new_cantitate = cantitate + 1;
                string query1 = "update BookTbl set Cantitate = " + new_cantitate + " where numeCarte = '" + BookID.SelectedValue.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(IssueNUM.Text == "")
            {
                MessageBox.Show("Enter the IssueNumber");
            }
            Con.Open();
            string query = "Delete IssueTbl where IssueNum = " + IssueNUM.Text + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Issue Succesfully Cancelled!");
            Con.Close();
            UpdateBookRemove();
            populate();
        }
    }
}
