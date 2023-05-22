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

namespace UniLibrary
{
    public partial class Report : Form
    {
        SqlConnection con = new SqlConnection("Data Source =amralahwany\\mssqlserver01; Initial Catalog =Final; Integrated Security = True");

        public Report()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.Show();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand query = new SqlCommand("SELECT COUNT(DISTINCT Student_ID) AS TotalStudents," +
                "COUNT(*) AS TotalBorrowedBooks," +
                "SUM(Book.price) AS TotalBorrowedBooksPrice," +
                "(SELECT TOP 1 Book_Name FROM Book GROUP BY Book_Name ORDER BY COUNT(*) DESC) AS MostFrequentBook," +
                "(SELECT TOP 1 Category.Book_Category FROM Category INNER JOIN Book ON Category.Book_ID = Book.Book_ID GROUP BY Category.Book_Category ORDER BY COUNT(*) DESC) AS MostFrequentCategory," +
                "(SELECT TOP 1 Student.Student_Name FROM Student INNER JOIN BorrowedBook ON Student.Student_ID = BorrowedBook.Student_ID GROUP BY Student.Student_Name ORDER BY COUNT(*) DESC) AS MostFrequentStudent " +
                "FROM BorrowedBook INNER JOIN Book ON BorrowedBook.Book_ID = Book.Book_ID;", con);

            // Execute the query and retrieve the data using a SqlDataReader
            SqlDataReader reader = query.ExecuteReader();
            reader.Read();

            label9.Text = reader.GetInt32(0).ToString();
            label10.Text = reader.GetInt32(1).ToString();
            label11.Text = reader.GetDouble(2).ToString();
            label12.Text = reader.GetString(3);
            label13.Text = reader.GetString(4);
            label14.Text = reader.GetString(5);


            con.Close();
        }
    }
}
