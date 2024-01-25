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
    public partial class FilterRoomsForm : Form
    {
        String type;
        public FilterRoomsForm(String type)
        {
            InitializeComponent();
            this.type = type;
        }

        SqlConnection con;
        SqlCommand cmd;

        string name;
        public void uname(string name)
        {
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click_1(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRooms_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void FilterRoomsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAvailability.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPoolAvl.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRooms.DropDownStyle = ComboBoxStyle.DropDownList;

            groupBox1.Visible = false;
            groupBox2.Visible = false;

            if (type == "Reception")
            {
                
                pictureBox3.Visible = false;
                label1.Visible = false;
                //pictureBox5.Visible = false;
                //label3.Visible = false;

            }
        }

        private void btnCheck_Click_2(object sender, EventArgs e)
        {
            //checkBox3.Checked = true; //new
            //showFilteredRooms();




            checkBox3.Checked = false;
            comboBoxRooms.Text = string.Empty;
            //dataGridView1.Visible = false;

            bool isFiltered = false;

            try
            {
                comboBoxRooms.Items.Clear();

                List<string> columns = new List<string>();
                List<Control> controls = new List<Control>();


                if (!string.IsNullOrEmpty(comboBoxRoomType.Text))
                {
                    columns.Add("roomType");
                    controls.Add(comboBoxRoomType);
                    isFiltered = true;
                }
                if (!string.IsNullOrEmpty(comboBoxAvailability.Text))
                {
                    columns.Add("availability");
                    controls.Add(comboBoxAvailability);
                    isFiltered = true;
                }
                if (!string.IsNullOrEmpty(txtFloor.Text))
                {
                    columns.Add("Floor");
                    controls.Add(txtFloor);
                    isFiltered = true;
                }
                if (!string.IsNullOrEmpty(comboBoxPoolAvl.Text))
                {
                    columns.Add("IsPoolAvailable");
                    controls.Add(comboBoxPoolAvl);
                    isFiltered = true;
                }

                if (isFiltered)
                {
                    string sql = "SELECT roomNumber FROM tblRoomStatus WHERE ";

                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (i == columns.Count - 1) sql += $" {columns[i]}=@{columns[i]}{i + 1};";
                        else sql += $" {columns[i]}=@{columns[i]}{i + 1} AND ";
                    }

                    OpenConnection(sql);

                    for (int i = 0; i < columns.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@{columns[i]}{i + 1}", controls[i].Text);
                    }

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dap.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        comboBoxRooms.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occured, {ex.Message}");
            }
        }

        //new
        public void showFilteredRooms()
        {
            
        }



        private void comboBoxRoomType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBoxAvailability_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtFloor_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPoolAvl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxRooms_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            checkBox3.Checked = false;
            checkBox2.Checked = false;
            //dataGridView1.Visible = true;

            OpenConnection("SELECT * FROM tblRoomStatus WHERE roomNumber = @rhn");

            cmd.Parameters.AddWithValue("@rhn", comboBoxRooms.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            DataTable dt = new DataTable();

            dt.Columns.Add("roomNumber");
            dt.Columns.Add("roomType");
            dt.Columns.Add("availability");
            dt.Columns.Add("Floor");
            dt.Columns.Add("IsPoolAvailable");

            dt.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4));

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void checkBox2_CheckedChanged_2(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox3.Checked = false;
                //groupBox4.Enabled = false;
                //groupBox2.Enabled = false;
                
                checkBox3.Checked = false;

                OpenConnection("SELECT * FROM tblRoomStatus");

                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

                con.Close();
            }
            else
            {
                groupBox4.Enabled = true;
                groupBox2.Enabled = true;
                dataGridView1.DataSource = null;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxRooms.Items.Count > 0 && checkBox3.Checked)
            {
                checkBox2.Checked = false;
                DataTable dt = new DataTable();

                dt.Columns.Add("roomNumber");
                dt.Columns.Add("roomType");
                dt.Columns.Add("availability");
                dt.Columns.Add("Floor");
                dt.Columns.Add("IsPoolAvailable");

                OpenConnection("SELECT * FROM tblRoomStatus WHERE roomNumber = @rhn");

                for (int i = 0; i < comboBoxRooms.Items.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@rhn", comboBoxRooms.Items[i]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    dt.Rows.Add(dr.GetValue(0), dr.GetValue(1), dr.GetValue(2), dr.GetValue(3), dr.GetValue(4));

                    cmd.Parameters.Clear();
                    dr.Close();
                }

                dataGridView1.DataSource = dt;

                con.Close();
            }
            else if (!checkBox3.Checked) dataGridView1.DataSource = null;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            MEEditForm form3 = new MEEditForm();
            form3.GetType(type);
            form3.uname(name);
            form3.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            MAproveFrom form5 = new MAproveFrom();
            form5.GetType(type);
            form5.uname(name);
            form5.Show();
            this.Hide();
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
            if(type== "Manager")
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
            else if(type== "Reception")
            {
                ReceptionForm form7 = new ReceptionForm(type, name);
                form7.Show();
                this.Hide();
            }
            
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

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            RoomEditForm roomEditForm = new RoomEditForm(type, name);
            roomEditForm.Show();
            this.Hide();
        }

        private void btnCheck_MouseHover(object sender, EventArgs e)
        {
            btnCheck.BackColor = Color.Orange;
        }

        private void btnCheck_MouseLeave(object sender, EventArgs e)
        {
            btnCheck.BackColor = Color.Gold;
        }
    }
}
