using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Data.SqlClient;

namespace UniLibrary
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = MALAK\\SQLEXPRESS01; Initial Catalog =Final; Integrated Security = True");
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the new form
            Register form2 = new Register();
            this.Hide();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Student WHERE Student_ID = @ID AND Password = @PW", con);
            cmd.Parameters.AddWithValue("@ID", textBox1.Text);
            cmd.Parameters.AddWithValue("@PW", textBox2.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable studentTable = new DataTable();
            da.Fill(studentTable);

            if (studentTable.Rows.Count > 0)
            {
                // If the entered ID and Password belong to a student, open the Student form
                Student student = new Student();
                this.Hide();
                student.Show();
            }
            else
            {
                // Check for admin credentials
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM Admin WHERE Admin_ID = @ID AND Admin_PW = @PW", con);
                cmd2.Parameters.AddWithValue("@ID", textBox1.Text);
                cmd2.Parameters.AddWithValue("@PW", textBox2.Text);

                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable adminTable = new DataTable();
                da2.Fill(adminTable);

                if (adminTable.Rows.Count > 0)
                {
                    // If the entered ID and Password belong toan admin, open the Admin form
                    Admin admin = new Admin();
                    admin.ShowDialog();
                    Hide();
                }
                else
                {
                    // If the entered ID and Password do not match any records in either the Student or Admin tables, display an error message
                    MessageBox.Show("Failed to login");
                }
            }
            con.Close();
        }
    }
}