using System.Data.SqlClient;
using System.Data;
namespace Biblioteca
{
    public partial class Login : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Biblioteca\dataBiblioteca\dateBiblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public Login()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loginName.Text = "";
            loginPassword.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;

            username = loginName.Text;
            password = loginPassword.Text;

            try
            {
                string query = "SELECT * FROM LibrarianTbl WHERE libName = '" + loginName.Text.Trim() + "' AND libPassword = '" + loginPassword.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = loginName.Text;
                    password = loginPassword.Text;

                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                Con.Close();
            }
            
        }
    }
}