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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelSystem
{
    public partial class RoomEditForm : Form
    {
        string type;
        string name;
        public RoomEditForm(string type,string name)
        {
            InitializeComponent();
            this.type = type;
            this.name = name;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox3.Visible = true;
        }

        private void RoomEditForm_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            easycode();
            easycode2();
            groupBox1.Visible = false;
            groupBox3.Visible = false;
        }
        string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
        public void easycode()
        {
            //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectiondb);
            connection.Open();
            string com = "select * from tblRoomStatus";
            SqlCommand sqlCommand = new SqlCommand(com, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            connection.Close();
        }

        public void easycode2()
        {
            //string connectiondb = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection conection = new SqlConnection(connectiondb);
            conection.Open();
            string sql = "SELECT top 1 * FROM tblRoomStatus order by roomNumber desc";
            SqlCommand sqlCommand = new SqlCommand(sql, conection);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            dr.Read();
            int c = Convert.ToInt32(dr.GetValue(0).ToString());
            this.textBox1.Text = (c + 1).ToString();
            conection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            groupBox3.Visible = false;
            //textBox7.Clear();

            SqlConnection connection = null;
            try
            {

                connection = new SqlConnection(connectiondb);
                connection.Open();


                string sql = "SELECT * FROM tblRoomStatus WHERE roomNumber=@roomNumber";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@roomNumber",Convert.ToInt16(textBox7.Text));

                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    this.textBox1.Text = dr.GetValue(0).ToString();
                    this.comboBox2.Text = dr.GetValue(1).ToString();
                    this.comboBox3.Text = dr.GetValue(2).ToString();
                    this.comboBox1.Text = dr.GetValue(3).ToString();
                    string pyesno = dr.GetValue(4).ToString();
                    if (pyesno == "yes")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (pyesno == "no")
                    {
                        radioButton2.Checked = true;
                    }
                    this.textBox4.Text = dr.GetValue(5).ToString();

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

        string pool;
        void poolchek()
        {
            if (radioButton1.Checked)
            {
                pool = "yes";
            }
            else if (radioButton2.Checked)
            {
                pool = "no";
            }
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            poolchek();

            try
            {

                string con2 = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(con2);
                connection.Open();
                string com = "insert into tblRoomStatus (roomNumber,roomType,availability,Floor,IsPoolAvailable,rcontact) values (@roomNumber,@roomType,@availability,@Floor,@IsPoolAvailable,@rcontact)";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {
                    
                    sqlCommand.Parameters.AddWithValue("@roomNumber",Convert.ToInt16(this.textBox1.Text));
                    sqlCommand.Parameters.AddWithValue("@roomType", this.comboBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@availability", this.comboBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@Floor", Convert.ToInt16(this.comboBox1.Text) );
                    sqlCommand.Parameters.AddWithValue("@IsPoolAvailable", pool);
                    sqlCommand.Parameters.AddWithValue("@rcontact", this.textBox4.Text);

                    int ret = sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("no of record inserted: " + ret);

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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {
                poolchek();

                // string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "update tblRoomStatus set roomType=@roomType,availability=@availability,Floor=@Floor,IsPoolAvailable=@IsPoolAvailable,rcontact=@rcontact where roomNumber=@roomNumber";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {


                    sqlCommand.Parameters.AddWithValue("@roomNumber", Convert.ToInt16(this.textBox1.Text));
                    sqlCommand.Parameters.AddWithValue("@roomType", this.comboBox2.Text);
                    sqlCommand.Parameters.AddWithValue("@availability", this.comboBox3.Text);
                    sqlCommand.Parameters.AddWithValue("@Floor", Convert.ToInt16(this.comboBox1.Text));
                    sqlCommand.Parameters.AddWithValue("@IsPoolAvailable", pool);
                    sqlCommand.Parameters.AddWithValue("@rcontact", textBox4.Text);

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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            try
            {

                //string con = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
                connection = new SqlConnection(connectiondb);
                connection.Open();
                string com = "delete from tblRoomStatus where roomNumber=@roomNumber";
                using (SqlCommand sqlCommand = new SqlCommand(com, connection))
                {

                    sqlCommand.Parameters.AddWithValue("@roomNumber", this.textBox1.Text);

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
            textBox1.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.ResetText();
            comboBox2.ResetText();
            comboBox3.ResetText();
            easycode2();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ManagerForm managerForm = new ManagerForm(type, name);
            managerForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.ResetText();
            comboBox2.ResetText();
            comboBox3.ResetText();
            easycode2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                string yesno = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                if (yesno == "yes")
                {
                    radioButton1.Checked = true;
                }
                else if (yesno == "no")
                {
                    radioButton2.Checked = true;
                }
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }
    }
}
