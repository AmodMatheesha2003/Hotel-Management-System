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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace HotelSystem
{
    public partial class roomChackingForm : Form
    {
        public roomChackingForm()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        int count = 0;
        DataSet ds;
        DataTable dt;
        DataTable dt2;
        string guestid;
        string type;

        public void GetGuestID(string id)
        {
            guestid = id;
        }

        public void GetRoomType(string type)
        {
            this.type = type;
        }
        string name;
        public void uname(string name)
        {
            this.name = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        public void OpenConnection(string sql)
        {
            string cs = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            con = new SqlConnection(cs);
            con.Open();

            cmd = new SqlCommand(sql, con);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void roomChackingForm_Load(object sender, EventArgs e)
        {
            dt2 = new DataTable();
            dt2.Columns.Add("roomNumber");
            dt2.Columns.Add("roomType");
            dt2.Columns.Add("Floor");
            dt2.Columns.Add("isPoolAvailable");
        }

        private void dataGridViewAvlRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dt2.Rows.Count > 0)
            {
                BookingForm bookingForm = new BookingForm();
                bookingForm.GetGuestID(guestid);
                bookingForm.GetTable(dt2);
                bookingForm.GetType(type);
                bookingForm.uname(name);
                bookingForm.Show();
                this.Hide();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("roomNumber");
                    dt.Columns.Add("roomType");
                    dt.Columns.Add("Floor");
                    dt.Columns.Add("IsPoolAvailable");

                    DataColumn checkBoxColumn = new DataColumn("CheckBoxColumn", typeof(bool));
                    checkBoxColumn.DefaultValue = false;
                    dt.Columns.Add(checkBoxColumn);

                    count = 1;
                }
                else
                {
                    dt.Rows.Clear();
                }

                if (checkBox1.Checked || checkBox2.Checked)
                {
                    if (checkBox1.Checked && checkBox2.Checked)
                    {
                        OpenConnection("SELECT roomNumber, roomType, Floor, IsPoolAvailable FROM tblRoomStatus WHERE availability=@avl");
                        cmd.Parameters.AddWithValue("@avl", "available");
                    }
                    else if (checkBox1.Checked)
                    {
                        OpenConnection("SELECT roomNumber, roomType, Floor, IsPoolAvailable FROM tblRoomStatus WHERE roomType=@rt AND availability=@avl");
                        cmd.Parameters.AddWithValue("@rt", "single");
                        cmd.Parameters.AddWithValue("@avl", "available");
                    }
                    else if (checkBox2.Checked)
                    {
                        OpenConnection("SELECT roomNumber, roomType, Floor, IsPoolAvailable FROM tblRoomStatus WHERE roomType=@rt AND availability=@avl");
                        cmd.Parameters.AddWithValue("@rt", "double");
                        cmd.Parameters.AddWithValue("@avl", "available");
                    }

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    dap.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dt.Rows.Add(ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3]);
                        }

                        dataGridView1.AllowUserToAddRows = false;

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                string roomNumber = dt2.Rows[i][0].ToString();
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    if (roomNumber == dt.Rows[j][0].ToString())
                                    {
                                        dt.Rows[j][4] = true;
                                        break;
                                    }
                                }
                            }
                        }

                        dataGridView1.DataSource = dt;
                        ds.Tables.Clear();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            finally
            {
                if (checkBox1.Checked || checkBox2.Checked) con.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["CheckBoxColumn"].Index && e.RowIndex != -1)
            {
                bool currentCheckboxValue = (bool)dataGridView1.Rows[e.RowIndex].Cells["CheckBoxColumn"].Value;
                dataGridView1.Rows[e.RowIndex].Cells["CheckBoxColumn"].Value = !currentCheckboxValue;

                if ((bool)dataGridView1.Rows[e.RowIndex].Cells["CheckBoxColumn"].Value)
                {
                    dt2.Rows.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value, dataGridView1.Rows[e.RowIndex].Cells[1].Value,
                                 dataGridView1.Rows[e.RowIndex].Cells[2].Value, dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                }
                else
                {
                    string removeRoomNumber = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (dt2.Rows[i][0].ToString() == removeRoomNumber)
                        {
                            dt2.Rows.Remove(dt2.Rows[i]);
                            break;
                        }
                    }
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            GuestForm form6 = new GuestForm();
            form6.GetType(type);
            form6.username(name);
            form6.Show();
            this.Hide();
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

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Orange;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
        }
    }
}
