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
    public partial class CleanerForm : Form
    {
        string name;
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds;
        string text;
        int len = 0;

        public CleanerForm(string name)
        {
            InitializeComponent();
            this.name = name;
        }

        

        public void OpenConnection(string sql)
        {
            //string cs = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
            string cs = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand(sql, con);
        }

        public void LoadRoomsToBeCleaned()
        {
            OpenConnection("SELECT roomNumber, roomType, Floor FROM tblRoomStatus WHERE availability='to be cleaned'");
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            ds = new DataSet();
            dap.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void CleanerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewRoomsToBeCleaned_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void CleanerForm_Load_1(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            userdetail();
            label12.Text = name.ToString();
            groupBox4.Visible = false;
            button2.Enabled = false;
            LoadRoomsToBeCleaned();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            text = "Welcome, " + name.ToString() + "!";
            label14.Text = " ";
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(comboBox1.Text == string.Empty))
            {
                comboBox1.Enabled = false;
                OpenConnection("UPDATE tblRoomStatus SET availability='cleaning' WHERE roomNumber=@rn");
                cmd.Parameters.AddWithValue("@rn", comboBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                button2.Enabled = true;
                LoadRoomsToBeCleaned();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            OpenConnection("UPDATE tblRoomStatus SET availability='available' WHERE roomNumber=@rn");
            cmd.Parameters.AddWithValue("@rn", comboBox1.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show($"Room number {comboBox1.Text} cleaned");
            comboBox1.Items.Remove(comboBox1.Text);
            comboBox1.Text = string.Empty;
            comboBox1.Enabled = true;
            con.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {                    
            if (!groupBox4.Visible)
            {
                groupBox4.Visible = true;
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                panel6.Visible = false;
                panel1.Visible = false;
                panel2.Visible = false;
                panel4.Visible = false;               
            }
            else
            {
                groupBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox4.Visible = false;
                panel6.Visible = true;
                panel1.Visible = true;
                panel2.Visible = true;
                panel4.Visible = true;
            }
        }

        private void CleanerForm_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox4.Visible = false;
            panel6.Visible = true;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
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
                    this.label16.Text = dr.GetValue(0).ToString();
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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Orange;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
        }
    }
}
