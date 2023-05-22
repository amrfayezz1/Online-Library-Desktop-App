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
    public partial class Register : Form
    {
        SqlConnection con = new SqlConnection("Data Source =MALAK\\mssqlserver01; Initial Catalog =Final; Integrated Security = True");
        public Register()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox6.Text))
                {
                    MessageBox.Show("Cannot register: Some fields are empty");
                    return;
                }


                if (radioButton1.Checked)
                {
                    // Check if the ID already exists
                    SqlCommand checkIDCmd2 = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE Admin_ID = @ID", con);
                    checkIDCmd2.Parameters.AddWithValue("@ID", textBox1.Text);
                    int idCountAD = (int)checkIDCmd2.ExecuteScalar();
                    if (idCountAD > 0)
                    {
                        MessageBox.Show("Cannot register: ID already exists");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Admin (Admin_ID, Admin_Name, Admin_PW, Admin_email) VALUES (@ID, @Name, @PW, @Mail)", con);
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PW", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Mail", textBox2.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (radioButton2.Checked)
                {
                    // Check if the ID already exists
                    SqlCommand checkIDCmd = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Student_ID = @ID", con);
                    checkIDCmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    int idCountStud = (int)checkIDCmd.ExecuteScalar();
                    if (idCountStud > 0)
                    {
                        MessageBox.Show("Cannot register: ID already exists");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (Student_ID, Student_Name, Password, Email) VALUES (@ID, @Name, @PW, @Email)", con);
                    cmd.Parameters.AddWithValue("@ID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PW", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox2.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("INSERT INTO StudentPhone (Student_ID, Phone_num) VALUES (@ID2, @Phone)", con);
                    cmd2.Parameters.AddWithValue("@ID2", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@Phone", textBox6.Text);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Cannot Register, Are you an admin or a student?");
                    return;
                }

                MessageBox.Show("Account Added");
                Form1 login = new Form1();
                this.Hide();
                login.Show();
            }
            finally { con.Close(); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();
            this.Hide();
            login.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
