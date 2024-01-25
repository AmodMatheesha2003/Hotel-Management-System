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
    public partial class RoomFilterForm : Form
    {
        public RoomFilterForm()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;

        public void OpenConnection(string sql)
        {
            string cs = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
            con = new SqlConnection(cs);
            con.Open();

            cmd = new SqlCommand(sql, con);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            comboBoxRooms.Text = string.Empty;
            dataGridView1.Visible = false;

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxRooms.Items.Count > 0 && checkBox1.Checked)
            {
                dataGridView1.Visible = true;
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
            else if (!checkBox1.Checked) dataGridView1.Visible = false;
        }

        private void comboBoxRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            dataGridView1.Visible = true;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                groupBox4.Enabled = false;
                groupBox2.Enabled = false;
                dataGridView2.Visible = true;
                checkBox1.Checked = false;

                OpenConnection("SELECT * FROM tblRoomStatus");

                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);

                dataGridView2.DataSource = ds.Tables[0];

                con.Close();
            }
            else
            {
                groupBox4.Enabled = true;
                groupBox2.Enabled = true;
                dataGridView2.Visible = false;
            }
        }
    }
}
