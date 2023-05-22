using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UniLibrary
{
    public partial class Admin : Form
    {
        SqlConnection con = new SqlConnection("Data Source =MALAK\\mssqlserver01; Initial Catalog =Final; Integrated Security = True");


        public Admin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Book_ID = int.Parse(textBox1.Text);
            string bookN = textBox3.Text;
            string bookAuthor = textBox6.Text;
            float price;
            float.TryParse(textBox5.Text, out price);
            int Copies = int.Parse(textBox4.Text);
            string bookCategory = textBox2.Text;

            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Book (Book.Book_ID, Book.Book_Name, Book.Book_Author, Book.price, Book.Number_Of_Copies) VALUES (@BookID, @Book_Name, @Book_Author, @Price, @Number_Of_Copies)", con);
            cmd.Parameters.AddWithValue("@BookID", Book_ID);
            cmd.Parameters.AddWithValue("@Book_Name", bookN);
            cmd.Parameters.AddWithValue("@Book_Author", bookAuthor);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Number_Of_Copies", Copies);

            int rowsAffected = cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("INSERT INTO Category (Category.Book_ID,Category.Book_category) VALUES (@BookID, @Book_category)", con);
            cmd2.Parameters.AddWithValue("@BookID", Book_ID);
            cmd2.Parameters.AddWithValue("@Book_category", bookCategory);
            int rowsAffected2 = cmd2.ExecuteNonQuery();
            con.Close();
            if (rowsAffected > 0 && rowsAffected2 > 0)
            {
                MessageBox.Show(" A new Book Added Successfully :) ");

                this.Close();
                Admin newAd = new Admin();
                newAd.Show();
            }
            else
            {
                MessageBox.Show(" Failed to add your Book:(  ");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Book_ID = int.Parse(textBox1.Text);
            string bookN = textBox3.Text;
            string bookAuthor = textBox6.Text;
            float price;
            float.TryParse(textBox5.Text, out price);
            int Copies = int.Parse(textBox4.Text);
            string bookCategory = textBox2.Text;

            con.Open();
            string cmd = "Update Book set Book_ID = @BookID , Book_Name=@BookName, Book_Author = @BookAuthor , price=@Bprice, Number_Of_Copies= @NumOfCopies where Book_ID=@BookID";
            SqlCommand cmd2 = new SqlCommand(cmd, con);
            cmd2.Parameters.AddWithValue("@BookID", Book_ID);
            cmd2.Parameters.AddWithValue("@BookName", bookN);
            cmd2.Parameters.AddWithValue("@BookAuthor", bookAuthor);
            cmd2.Parameters.AddWithValue("@Bprice", price);
            cmd2.Parameters.AddWithValue("@NumOfCopies", Copies);
            int rowsAffected = cmd2.ExecuteNonQuery();
            string cmd3 = "Update Category set Book_category=@BookCategory where Book_ID=@BookID";
            SqlCommand cmd4 = new SqlCommand(cmd3, con);
            cmd4.Parameters.AddWithValue("@BookCategory", bookCategory);
            cmd4.Parameters.AddWithValue("@BookID", Book_ID);
            int rowsAffected1 = cmd4.ExecuteNonQuery();
            con.Close();
            if (rowsAffected1 > 0 && rowsAffected > 0)
            {
                MessageBox.Show(" Book " + bookN + " updated Successfully :) ");

                this.Close();
                Admin newAd = new Admin();
                newAd.Show();
            }
            else
            {
                MessageBox.Show("failed to update");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Book_ID = int.Parse(textBox1.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete From Book where Book_ID=@BookID", con);
            cmd.Parameters.AddWithValue("@BookID", Book_ID);
            cmd.ExecuteNonQuery();
            con.Close();

            this.Close();
            Admin newAd = new Admin();
            newAd.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Book.Book_ID, Book_Name,Book_Author, price,Number_Of_Copies,Book_category FROM Book, Category where Number_Of_Copies>0 and Book.Book_ID= Category.Book_ID", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.ReadOnly = true;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.Columns[0].HeaderText = "BookID";
            dataGridView2.Columns[1].HeaderText = "Title";
            dataGridView2.Columns[2].HeaderText = "Author";
            dataGridView2.Columns[3].HeaderText = "Price";
            dataGridView2.Columns[4].HeaderText = "Copies";
            dataGridView2.Columns[5].HeaderText = "Category";
            SqlCommand cmd2 = new SqlCommand("Select * from BorrowedBook", con);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd2);
            DataTable dt1 = new DataTable();
            adapter1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[0].HeaderText = "BookID";
            dataGridView1.Columns[1].HeaderText = "StudentID";
            dataGridView1.Columns[2].HeaderText = "FromDate";
            dataGridView1.Columns[3].HeaderText = "No_OF_Days";
            dataGridView1.Columns[4].HeaderText = "To_Date";
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report rpt = new Report();
            rpt.Show();
        }
    }
}
