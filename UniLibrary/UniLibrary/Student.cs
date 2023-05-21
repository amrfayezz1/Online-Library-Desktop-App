using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.VariantTypes;
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
        SqlConnection con = new SqlConnection("Data Source = MALAK\\SQLEXPRESS01; Initial Catalog =Final; Integrated Security = True");

        public Student()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Student_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Book", con);
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
            command.Parameters.AddWithValue("@Phone", textBox6.Text);
            command.Parameters.AddWithValue("@ID", textBox3.Text);

            int rowsAffected = command.ExecuteNonQuery();

            // Update the StudentPhone table
            SqlCommand cmd = new SqlCommand("UPDATE StudentPhone SET Phone_num = @Phone WHERE Student_ID = @ID", con);
            cmd.Parameters.AddWithValue("@Phone", textBox6.Text);
            cmd.Parameters.AddWithValue("@ID", textBox3.Text);
            int rowsAffected2 = cmd.ExecuteNonQuery();

            // Close the connection
            con.Close();

            // Display a message indicating the number of rows affectedby the update
            if (rowsAffected > 0 && rowsAffected2 > 0)
            {
                MessageBox.Show("Student data updated successfully!");
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
            int studID = int.Parse(textBoxBorrower.Text);
            DateTime from = From.Value;
            DateTime to = To.Value;
            int numDays = (int)(to - from).TotalDays;
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
                    MessageBox.Show("Book reserved successfully!");
                }
                else
                {
                    MessageBox.Show("An error occurred while reserving the book.");
                }
            }
            else { MessageBox.Show("Date invalid"); return; }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
