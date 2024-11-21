using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentsForm student = new StudentsForm();
            student.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BooksForm book = new BooksForm();
            book.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LibrariansForm librarian = new LibrariansForm();
            librarian.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IssueBookForm issuebook = new IssueBookForm();
            issuebook.Show();
            this.Hide();
        }
    }
}
