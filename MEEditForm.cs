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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace HotelSystem
{
    
    public partial class MEEditForm : Form
    {
        public MEEditForm()
        {
            InitializeComponent();
        }

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
        private void button2_Click(object sender, EventArgs e)
        {
            
            ManagerForm form2 = new ManagerForm(type, name);
            form2.Show();
            this.Hide();
        }
        public string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            easycode();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            combo();


            textBox1.ReadOnly = true;
        }
        void combo()
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

        public void easycode()
        {
            //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectiondb);
            connection.Open();
            string com = "select * from Table1";
            SqlCommand sqlCommand = new SqlCommand(com, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            /*groupBox2.Visible = false;
            if (!groupBox1.Visible)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }*/
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            /* groupBox1.Visible = false;
             if (!groupBox2.Visible)
             {
                 groupBox2.Visible = true;
             }
             else
             {
                 groupBox2.Visible = false;
             }*/
        }

        private void Form3_MouseClick(object sender, MouseEventArgs e)
         {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            //groupBox1.Visible = false;
            //groupBox2.Visible = false;
        }

         private void pictureBox3_DoubleClick(object sender, EventArgs e)
         {

         }

         private void pictureBox8_Click(object sender, EventArgs e)
         {
             //MAproveFrom form5 = new MAproveFrom();
             //form5.GetType(type);
             //form5.uname(name);
             //form5.Show();
             //this.Hide();
         }

         private void pictureBox9_Click(object sender, EventArgs e)
         {
            // FilterRoomsForm frm = new FilterRoomsForm(type);
             //frm.uname(name);
             //frm.Show();
             //this.Hide();
         }

         private void textBox1_TextChanged(object sender, EventArgs e)
         {

         }

         private void pictureBox4_Click(object sender, EventArgs e)
         {
             /*
             GuestForm form6 = new GuestForm();

             form6.GetType(type);
             form6.username(name);

             form6.Show();
             this.Hide();*/
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            //ManagerForm form2 = new ManagerForm(type, name);
            //form2.Show();
            //this.Hide();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Orange;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Gold;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            //button3.BackColor = Color.Orange;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            //button3.BackColor = Color.Gold;
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {
                //string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "insert into Table1(uid,username,password,type,Phone_Number,NIC_Number,Address) values (@uid,@username,@password,@type,@Phone_Number,@NIC_Number,@Address)";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    if(textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0 && textBox5.Text.Length > 0 && textBox6.Text.Length > 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@uid", this.textBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@username", this.textBox2.Text);
                        sqlCommand.Parameters.AddWithValue("@password", this.textBox3.Text);
                        sqlCommand.Parameters.AddWithValue("@type", this.comboBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox4.Text);
                        sqlCommand.Parameters.AddWithValue("@NIC_Number", this.textBox5.Text);
                        sqlCommand.Parameters.AddWithValue("@Address", this.textBox6.Text);
                        int ret = sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("no of record inserted: " + ret);
                    }
                    MessageBox.Show("Please Fill all fields!");
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
            easycode();
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.ResetText();
            combo();
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            if (!groupBox1.Visible)
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            //dataGridView1.Size = new NewStruct(1139, 341);
            groupBox2.Visible = false;
            //textBox7.Clear();
            //dataGridView1.Visible = true;
            //groupBox2.Visible = true;

            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(connectiondb);
                connection.Open();


                string sql = "SELECT * FROM Table1 WHERE NIC_Number=@NIC";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@NIC", textBox7.Text);

                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    this.textBox1.Text = dr.GetValue(0).ToString();
                    this.textBox2.Text = dr.GetValue(1).ToString();
                    this.textBox3.Text = dr.GetValue(2).ToString();
                    this.comboBox1.Text = dr.GetValue(3).ToString();
                    this.textBox4.Text = dr.GetValue(4).ToString();
                    this.textBox5.Text = dr.GetValue(5).ToString();
                    this.textBox6.Text = dr.GetValue(6).ToString();
                }
                else
                {
                    MessageBox.Show("No rows found for the specified status.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "delete from Table1 where uid=@uid";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {


                    sqlCommand.Parameters.AddWithValue("@uid", this.textBox1.Text);


                    int ret = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("no of record delete: " + ret);
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
            easycode();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.ResetText();
            combo();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {

                // string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "update Table1 set username=@username,password=@password,type=@type,Phone_Number=@Phone_Number,NIC_Number=@NIC_Number,Address=@Address where uid=@uid";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {


                    sqlCommand.Parameters.AddWithValue("@uid", this.textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@username", this.textBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@password", this.textBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@type", this.comboBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox4.Text);
                    sqlCommand.Parameters.AddWithValue("@NIC_Number", this.textBox5.Text);
                    sqlCommand.Parameters.AddWithValue("@Address", this.textBox6.Text);

                    int ret = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("no of record updated: " + ret);
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
            easycode();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ManagerForm managerForm = new ManagerForm(type,name);
            managerForm.Show();
            this.Hide();
        }
    }
}
