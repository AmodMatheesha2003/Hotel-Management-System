using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelSystem
{
    public partial class MAproveFrom : Form
    {
        public MAproveFrom()
        {
            InitializeComponent();
        }
        string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
        string type;
        public void GetType(string type)
        {
            this.type = type;
        }
        string name;
        public void uname(string name)
        {
            this.name = name;
        }
        public void textload()
        {
            SqlConnection conection = new SqlConnection(connectiondb);

            conection.Open();
            string sql = "SELECT top 1 * FROM Table1 order by uid desc";
            SqlCommand sqlCommand = new SqlCommand(sql, conection);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            dr.Read();
            int c = Convert.ToInt32(dr.GetValue(0).ToString());
            this.textBox1.Text = "0" + (c + 1).ToString();
            conection.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //textload();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            resetcom();

        }
        void resetcom()
        {
            SqlConnection connection = new SqlConnection(connectiondb);
            connection.Open();
            string status = "tocheck";
            string sql = "SELECT NIC_Number FROM Table2 where status=@status";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);
            sqlCommand.Parameters.AddWithValue("@status", status);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr.GetValue(0));
            }
            dr.Close();
            connection.Close();
        }

        void easy1()
        {
            textload();
            SqlConnection connection = null;
            try
            {
                //string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();

                
                string sql = "SELECT * FROM Table2 WHERE NIC_Number=@NIC_Number";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@NIC_Number", comboBox1.Text);

                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    //this.textBox1.Text = dr.GetValue(0).ToString();
                    this.textBox2.Text = dr.GetValue(0).ToString();
                    this.textBox3.Text = dr.GetValue(1).ToString();
                    this.textBox7.Text = dr.GetValue(2).ToString();
                    this.textBox5.Text = dr.GetValue(3).ToString();
                    this.textBox4.Text = dr.GetValue(4).ToString();
                    this.textBox6.Text = dr.GetValue(5).ToString();
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

            
            textBox1.ReadOnly = true;

        } 

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            easy1();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void cleartex()
        {
            comboBox1.ResetText();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            //textload();

            SqlConnection connection = null;

            try
            {
                //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "insert into Table1(uid,username,password,type,Phone_Number,NIC_Number,Address) values (@uid,@username,@password,@type,@Phone_Number,@NIC_Number,@Address)";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@uid", this.textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@username", this.textBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@password", this.textBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@type", this.textBox7.Text);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox5.Text);
                    sqlCommand.Parameters.AddWithValue("@NIC_Number", this.textBox4.Text);
                    sqlCommand.Parameters.AddWithValue("@Address", this.textBox6.Text);
                    int ret = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("no of record inserted: " + ret);

                }
                string status2 = "approve";
                string com2 = "update Table2 set status=@status where NIC_Number=@NIC_Number";
                SqlCommand sqlCommand2 = new SqlCommand(com2, connection);
                sqlCommand2.Parameters.AddWithValue("@NIC_Number", this.textBox4.Text);
                sqlCommand2.Parameters.AddWithValue("@status", status2);
                sqlCommand2.ExecuteNonQuery();
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
                cleartex();
                //resetcom();

            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;
            try
            {
                //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();

                string status2 = "Decline";
                string com2 = "update Table2 set status=@status where NIC_Number=@NIC_Number";

                using (SqlCommand sqlCommand2 = new SqlCommand(com2, connection))
                {
                    sqlCommand2.Parameters.AddWithValue("@NIC_Number", this.textBox4.Text);
                    sqlCommand2.Parameters.AddWithValue("@status", status2);

                    int rowsAffected = sqlCommand2.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("no of record deleted: " + rowsAffected);
                    }
                    else
                    {
                        Console.WriteLine("No rows updated. Check your parameters or the existence of the record.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                // Ensure connection is closed, even if an exception occurs
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                cleartex();
                //resetcom();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            GuestForm form6 = new GuestForm();

            form6.GetType(type);
            form6.username(name);

            form6.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            FilterRoomsForm frm = new FilterRoomsForm(type);
            frm.uname(name);
            frm.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MEEditForm form3 = new MEEditForm();
            form3.GetType(type);
            form3.uname(name);
            form3.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            if (!groupBox1.Visible)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            if (!groupBox2.Visible)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void Form5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            RoomEditForm roomEditForm = new RoomEditForm(type,name);
            roomEditForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            ManagerForm form2 = new ManagerForm(type, name);
            form2.Show();
            this.Hide();
        }
    }
}
