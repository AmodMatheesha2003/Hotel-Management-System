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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace HotelSystem
{
    

    public partial class GuestForm : Form
    {
        
        string guestID;
        
        public GuestForm()
        {
            InitializeComponent();
        }

        string type;
        public void GetType(string type)
        {
            this.type = type;
        }
        string name;
        public void username(string name)
        {
            this.name = name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            
        }
        string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            groupBox3.Visible = false;
            //textBox7.Clear();
            //dataGridView1.Visible = true;
            //groupBox2.Visible = true;

            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(connectiondb);
                connection.Open();


                string sql = "SELECT * FROM Table3 WHERE NIC=@NIC";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@NIC", textBox7.Text);

                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    this.textBox1.Text = dr.GetValue(0).ToString();
                    this.textBox2.Text = dr.GetValue(1).ToString();
                    this.textBox3.Text = dr.GetValue(2).ToString();
                    this.textBox4.Text = dr.GetValue(3).ToString();
                    string gender = dr.GetValue(4).ToString();
                    if (gender == "Male")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (gender == "Female")
                    {
                        radioButton2.Checked = true;
                    }
                    this.textBox6.Text = dr.GetValue(5).ToString();

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
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            easycode();
            easycode2();
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            textBox1.ReadOnly = true;

        }
        public void easycode2()
        {
            //string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection conection = new SqlConnection(connectiondb);
            conection.Open();
            string sql = "SELECT top 1 * FROM Table3 order by guestid desc";
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
            string com = "select * from Table3";
            SqlCommand sqlCommand = new SqlCommand(com, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                string gender = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                if (gender == "Male")
                {
                    radioButton1.Checked = true;
                }
                else if (gender == "Female")
                {
                    radioButton2.Checked = true;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {

                //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "insert into Table3 (guestid,Name,NIC,Phone_Number,Gender,Address) values (@guestid,@Name,@NIC,@Phone_Number,@Gender,@Address)";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    string gender;

                    if (radioButton1.Checked) { gender = "Male"; }
                    else if (radioButton2.Checked) { gender = "Female"; }
                    else { gender = string.Empty; }

                    if(textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox6.Text.Length > 0 && textBox4.Text.Length > 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@guestid", this.textBox1.Text);
                        sqlCommand.Parameters.AddWithValue("@Name", this.textBox2.Text);
                        sqlCommand.Parameters.AddWithValue("@NIC", this.textBox3.Text);
                        sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox4.Text);
                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
                        sqlCommand.Parameters.AddWithValue("@Address", this.textBox6.Text);

                        int ret = sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("no of record inserted: " + ret);
                    }

                    MessageBox.Show("Please fill all fields!");
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {

                // string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "update Table3 set Name=@Name,NIC=@NIC,Phone_Number=@Phone_Number,Gender=@Gender,Address=@Address where guestid=@guestid";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    string gender;

                    if (radioButton1.Checked) { gender = "Male"; }
                    else if (radioButton2.Checked) { gender = "Female"; }
                    else { gender = string.Empty; }

                    sqlCommand.Parameters.AddWithValue("@guestid", this.textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@Name", this.textBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@NIC", this.textBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", this.textBox4.Text);
                    sqlCommand.Parameters.AddWithValue("@Gender", gender);
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //manager reception only
            if (type == "Manager")
            {
                ManagerForm form = new ManagerForm(type,name);
                form.Show();
                this.Hide();
            }
            else if (type == "Reception")
            {
                ReceptionForm form7 = new ReceptionForm(type,name);
                form7.Show();
                this.Hide();
            }
            else
            {
                //Form1 form1 = new Form1();
                //form1.Show();
                //this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            easycode2();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            //string server = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectiondb);

            try
            {
                sqlConnection.Open();
                string com = "SELECT * FROM Table3 WHERE guestid=@guestid AND NIC = @NIC AND Name = @Name AND Phone_Number = @Phone_Number";

                using (SqlCommand sqlCommand = new SqlCommand(com, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@guestid", textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("@NIC", textBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@Name", textBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@Phone_Number", textBox4.Text);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            guestID = textBox1.Text;
                            roomChackingForm form = new roomChackingForm();
                            form.GetGuestID(guestID);
                            form.GetRoomType(type);
                            form.uname(name);
                            form.Show();
                            this.Hide();


                        }
                        else
                        {
                            MessageBox.Show("Pleasse enter guest detail first");

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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            
            if (!groupBox1.Visible)
            {
                groupBox1.Visible = true;
                groupBox3.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
                groupBox3.Visible = false;
            }
            
            //dataGridView1.Visible = false;
            //groupBox2.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {

                //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "delete from Table3 where guestid=@guestid";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {


                    sqlCommand.Parameters.AddWithValue("@guestid", this.textBox1.Text);


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
            textBox6.Clear();
            textBox7.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            easycode2();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.BackColor = Color.Orange;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.Gold;
        }

        private void GuestForm_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox1.Visible = false;
            groupBox3.Visible = false;
        }
    }
}
