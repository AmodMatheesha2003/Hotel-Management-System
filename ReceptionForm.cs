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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HotelSystem
{
    public partial class ReceptionForm : Form
    {
        string type;
        string name;
        string text;
        int len = 0;
        public ReceptionForm(string type,string name)
        {
            InitializeComponent();
            this.type = type;
            this.name = name;
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            text = "Welcome, " + name.ToString() + "!";
            label14.Text = " ";
            timer1.Start();

            label27.Text = name;
            userdetail();
            groupBox4.Visible = false;
        }
        void userdetail()
        {
            SqlConnection connection = null;
            try
            {
                string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();


                string sql = "SELECT * FROM Table1 WHERE username=@username";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@username", name);

                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    this.label24.Text = dr.GetValue(0).ToString();
                    this.label17.Text = dr.GetValue(4).ToString();
                    this.label18.Text = dr.GetValue(5).ToString();
                    this.label19.Text = dr.GetValue(6).ToString();

                }
                else
                {
                    Console.WriteLine("No rows found for the specified status.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void guestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            GuestForm form6 = new GuestForm();
            form6.GetType(type);
            form6.username(name);
            form6.Show();
            this.Hide();
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FilterRoomsForm frm = new FilterRoomsForm(type);
            frm.uname(name);
            frm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (!groupBox4.Visible)
            {
                groupBox1.Visible = false;

                groupBox4.Visible = true;
            }
            else
            {
                groupBox1.Visible = true;
                //groupBox2.Visible = true;
                groupBox4.Visible = false;
            }
        }

        private void ReceptionForm_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox4.Visible = false;
            groupBox1.Visible = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (len < text.Length)
            {
                label14.Text = label14.Text + text.ElementAt(len);
                len++;
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
