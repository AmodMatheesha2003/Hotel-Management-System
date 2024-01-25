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
    public partial class EmployeeSubmit : Form
    {
        public EmployeeSubmit()
        {
            InitializeComponent();
        }

        string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            subbuttn();
            

            
        }
        public void subbuttn()
        {
            SqlConnection connection = null;

            try
            {
                string status = "tocheck";

                connection = new SqlConnection(con);
                connection.Open();
                string com = "insert into Table2(username,password,type,Phone_Number,NIC_Number,Address,status) values (@username,@password,@type,@Phone_Number,@NIC_Number,@Address,@status)";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    //sqlCommand.Parameters.AddWithValue("@uid", this.textBox1.Text);
                    if(textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && comboBox1.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox6.Text.Length > 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@username", this.textBox2.Text);
                        sqlCommand.Parameters.AddWithValue("@password", this.textBox3.Text);
                        sqlCommand.Parameters.AddWithValue("@type", this.comboBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox4.Text);
                        sqlCommand.Parameters.AddWithValue("@NIC_Number", this.textBox5.Text);
                        sqlCommand.Parameters.AddWithValue("@Address", this.textBox6.Text);
                        sqlCommand.Parameters.AddWithValue("@status", status);
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("wait for approval from a manager..");

                        LoginForm form1 = new LoginForm();
                        form1.Show();
                        this.Hide();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            //string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
           // SqlConnection conection = new SqlConnection(con);
            //conection.Open();
            //string sql = "SELECT top 1 * FROM Table1 order by uid desc";
           // SqlCommand sqlCommand = new SqlCommand(sql, conection);
            //SqlDataReader dr = sqlCommand.ExecuteReader();
            //dr.Read();
            //int c = Convert.ToInt32(dr.GetValue(0).ToString());
            //this.textBox1.Text = "0" + (c + 1).ToString();
            //conection.Close();

            //textBox1.ReadOnly = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            //comboBox1.ResetText();

            
            //comboBox1.Text = string.Empty;
            comboBox1.ResetText();

            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            subbuttn();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Orange;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
