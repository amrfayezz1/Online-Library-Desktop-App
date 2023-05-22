using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UniLibrary
{
    public partial class Student : Form
    {
        SqlConnection con = new SqlConnection("Data Source =MALAK\\mssqlserver01; Initial Catalog =Final; Integrated Security = True");
        private int sID;
        public Student(int id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            sID = id;
        }

        private void Student_Load(object sender, EventArgs e)
        {
            label12.Text = sID.ToString();

            // load books
            SqlCommand cmd = new SqlCommand("SELECT * FROM Book where Number_Of_Copies > 0", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[0].HeaderText = "BookID";
            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[2].HeaderText = "Author";
            dataGridView1.Columns[3].HeaderText = "Price";
            dataGridView1.Columns[4].HeaderText = "Copies";

            con.Close();
            // load user data
            con.Open();
            SqlCommand query = new SqlCommand("SELECT Student_Name, Password, Email FROM Student WHERE Student_ID = @ID", con);
            query.Parameters.AddWithValue("@ID", this.sID);

            // Execute the query and retrieve the data using a SqlDataReader
            SqlDataReader reader = query.ExecuteReader();
            reader.Read();

            textBox2.Text = reader.GetString(0);
            textBox3.Text = reader.GetString(1);
            textBox1.Text = reader.GetString(2);
            label12.Text = sID.ToString();


            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            // Update the Student table
            SqlCommand command = new SqlCommand("UPDATE Student SET Student_Name = @Username, Email = @Email WHERE Student_ID = @ID", con);
            command.Parameters.AddWithValue("@Username", textBox2.Text);
            command.Parameters.AddWithValue("@Email", textBox1.Text);
            command.Parameters.AddWithValue("@ID", sID);

            int rowsAffected = command.ExecuteNonQuery();

            // Close the connection
            con.Close();

            // Display a message indicating the number of rows affectedby the update
            if (rowsAffected > 0)
            {
                MessageBox.Show("Student data updated successfully!");
                this.Close();
                Student newSt = new Student(this.sID);
                newSt.Show();
            }
            else
            {
                MessageBox.Show("An error occurred while updating the student data.");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int bookID = int.Parse(textBoxBook.Text);
            int studID = this.sID;
            DateTime from = From.Value;
            DateTime to = To.Value;
            int numDays = (int)(to - from).TotalDays;

            double price;

            SqlCommand query = new SqlCommand("SELECT price FROM Book WHERE Book_ID = @ID", con);
            query.Parameters.AddWithValue("@ID", bookID);

            // Execute the query and retrieve the data using a SqlDataReader
            con.Open();
            SqlDataReader reader = query.ExecuteReader();
            reader.Read();
            price = reader.GetDouble(0);
            con.Close();

            if (numDays > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO BorrowedBook (Book_ID, Student_ID, From_D, Num_Of_days, TO_D) VALUES (@BookID, @StudentID, @FromDate, @NumDays, @ToDate)", con);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.Parameters.AddWithValue("@StudentID", studID);
                cmd.Parameters.AddWithValue("@FromDate", from);
                cmd.Parameters.AddWithValue("@NumDays", numDays);
                cmd.Parameters.AddWithValue("@ToDate", to);
                int rowsAffected = cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("UPDATE Book SET Number_Of_Copies = Number_Of_Copies - 1 WHERE Book_ID = @BookID", con);
                cmd2.Parameters.AddWithValue("@BookID", bookID);
                int rowsAffected2 = cmd2.ExecuteNonQuery();

                con.Close();
                if (rowsAffected > 0 && rowsAffected2 > 0)
                {
                    con.Open();
                    SqlCommand queryZ = new SqlCommand("SELECT Student_ID FROM Dependent WHERE Student_ID = @ID", con);
                    queryZ.Parameters.AddWithValue("@ID", this.sID);

                    // Execute the query and retrieve the data using a SqlDataReader
                    SqlDataReader readerZ = queryZ.ExecuteReader();
                    if (readerZ.Read())
                    {
                        MessageBox.Show("Book reserved successfully for: " + price * 0.8 + " L.E. !\n(Dependent with 20% discount)");
                    }
                    else
                    {
                        MessageBox.Show("Book reserved successfully for: " + price + " L.E. !\n(Not Dependent)");
                    }
                    con.Close();
                    this.Close();
                    Student newSt = new Student(this.sID);
                    newSt.Show();
                }
                else
                {
                    MessageBox.Show("An error occurred while reserving the book.");
                }
            }
            else { MessageBox.Show("Date invalid"); return; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
