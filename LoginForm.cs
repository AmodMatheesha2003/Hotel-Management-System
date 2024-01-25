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

namespace HotelSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
               //comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //comboBox1.Text = "Option 1";
           


        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string server = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(server);

            try
            {
                sqlConnection.Open();
                string com = "SELECT * FROM Table1 WHERE type=@type AND username = @username AND password = @password";

                using (SqlCommand sqlCommand = new SqlCommand(com, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@type", comboBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@username", textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@password", textBox2.Text);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (comboBox1.Text == "Manager")
                            {
                                ManagerForm form2 = new ManagerForm(comboBox1.Text, textBox1.Text);
                                
                                form2.Show();
                                this.Hide();
                            }
                            else if (comboBox1.Text == "Reception")
                            {
                                ReceptionForm form7 = new ReceptionForm(comboBox1.Text, textBox1.Text);
                                form7.Show();
                                this.Hide();
                            }
                            else if (comboBox1.Text == "Cleaner")
                            {
                                CleanerForm cleaner = new CleanerForm(textBox1.Text);
                                cleaner.Show();
                                this.Hide();
                            }
                            else if(comboBox1.Text == "Cashier")
                            {
                                CashierForm cashier = new CashierForm(textBox1.Text);
                                cashier.Show();
                                this.Hide();
                            }

                        }
                        else
                        {
                            label4.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username")
            {
                textBox1.Text = " ";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            
        }

        private void textBox2_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            EmployeeSubmit form4 = new EmployeeSubmit();
            form4.Show();
            this.Hide();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Orange;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Orange;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gold;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.ResetText();
            label4.Visible = false;
            comboBox1.Text = "User Type";
            textBox1.Text = "Username";
            label1.Visible = true;
            label1.Text = "PASSWORD";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
