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

namespace HotelSystem
{
    public partial class BookingForm : Form
    {
        string guestid;
        DataTable dt2;
        SqlConnection con;
        SqlCommand cmd;
        string date;
        string type;
        public BookingForm()
        {
            InitializeComponent();
        }

        string name;
        public void uname(string name)
        {
            this.name = name;
        }

        public void GetGuestID(string id)
        {
            guestid = id;
        }

        public void GetTable(DataTable table)
        {
            dt2 = table;
        }

        public void GetType(string t)
        {
            this.type = t;
        }

        private void BookingForm_Load(object sender, EventArgs e)
        {
            pictureBox5.Visible = false; //
            pictureBox8.Visible = false; //

            orderedRoomNumbers.DropDownStyle = ComboBoxStyle.DropDownList;
            DateTime today = DateTime.Today;
            dateTimePicker1.Value = today;
            dateTimePicker1.Enabled = false;
            checkBoxcheckOutdateSame.Visible = false;
            checkBox1.Visible = false;

            if (dt2.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt2;
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                orderedRoomNumbers.Items.Add(dt2.Rows[i][0].ToString());
            }

            if (orderedRoomNumbers.Items.Count > 1)
            {
                checkBoxcheckOutdateSame.Visible = true;
                checkBox1.Visible = true;
            }
        }

        public void OpenConnection(string sql)
        {
            //string cs = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
            string cs = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            con = new SqlConnection(cs);
            con.Open();

            cmd = new SqlCommand(sql, con);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void orderedRoomNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxcheckOutdateSame_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void orderedRoomNumbers_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                OpenConnection("INSERT INTO tblBookedRooms VALUES(@rn, @cID, @food, @bar, @checkInD, @checkOutD, @child, @adult, @checkedOut)");

                for (int i = 0; i < orderedRoomNumbers.Items.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@rn", orderedRoomNumbers.Items[i]);
                    cmd.Parameters.AddWithValue("@cID", guestid);
                    cmd.Parameters.AddWithValue("@food", checkBoxFood.Checked ? "Yes" : "No");
                    cmd.Parameters.AddWithValue("@bar", checkBox3.Checked ? "Yes" : "No");
                    cmd.Parameters.AddWithValue("@checkInD", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@checkOutD", dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"));

                    cmd.Parameters.AddWithValue("@child", Convert.ToInt32(childCount.Value));
                    cmd.Parameters.AddWithValue("@adult", Convert.ToInt32(adultCount.Value));
                    cmd.Parameters.AddWithValue("@checkedOut", "no");

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                MessageBox.Show($"All {orderedRoomNumbers.Items.Count} rooms booked");
                orderedRoomNumbers.Text = string.Empty;
                con.Close();

                List<string> roomNumbers = new List<string>();
                OpenConnection("UPDATE tblRoomStatus SET availability='booked' WHERE roomNumber=@rn");

                for (int i = 0; i < orderedRoomNumbers.Items.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@rn", orderedRoomNumbers.Items[i]);
                    cmd.ExecuteNonQuery();
                    roomNumbers.Add(orderedRoomNumbers.Items[i].ToString());
                    cmd.Parameters.Clear();
                }

                int count = orderedRoomNumbers.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    orderedRoomNumbers.Items.Remove(roomNumbers[i]);
                }

                con.Close();
            }
            else
            {
                if (orderedRoomNumbers.Text.Length != 0)
                {
                    if (checkBoxcheckOutdateSame.Checked)
                    {
                        OpenConnection("INSERT INTO tblBookedRooms VALUES(@rn, @cID, @food, @bar, @checkInD, @checkOutD, @child, @adult, @checkedOut)");

                        cmd.Parameters.AddWithValue("@rn", orderedRoomNumbers.Text);
                        cmd.Parameters.AddWithValue("@cID", guestid);
                        cmd.Parameters.AddWithValue("@food", checkBoxFood.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@bar", checkBox3.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@checkInD", DateTime.Today.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@checkOutD", date);

                        cmd.Parameters.AddWithValue("@child", Convert.ToInt32(childCount.Value));
                        cmd.Parameters.AddWithValue("@adult", Convert.ToInt32(adultCount.Value));
                        cmd.Parameters.AddWithValue("@checkedOut", "no");

                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Room {orderedRoomNumbers.Text} booked");

                        con.Close();
                    }
                    else
                    {
                        OpenConnection("INSERT INTO tblBookedRooms VALUES(@rn, @cID, @food, @bar, @checkInD, @checkOutD, @child, @adult, @checkedOut)");

                        cmd.Parameters.AddWithValue("@rn", orderedRoomNumbers.Text);
                        cmd.Parameters.AddWithValue("@cID", guestid);
                        cmd.Parameters.AddWithValue("@food", checkBoxFood.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@bar", checkBox3.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@checkInD", DateTime.Today.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@checkOutD", dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"));

                        cmd.Parameters.AddWithValue("@child", Convert.ToInt32(childCount.Value));
                        cmd.Parameters.AddWithValue("@adult", Convert.ToInt32(adultCount.Value));
                        cmd.Parameters.AddWithValue("@checkedOut", "no");

                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Room {orderedRoomNumbers.Text} booked");

                        con.Close();
                    }

                    OpenConnection("UPDATE tblRoomStatus SET availability='booked' WHERE roomNumber=@rn");
                    cmd.Parameters.AddWithValue("@rn", orderedRoomNumbers.Text);
                    cmd.ExecuteNonQuery();

                    orderedRoomNumbers.Items.Remove(orderedRoomNumbers.Text);
                    orderedRoomNumbers.Text = string.Empty;

                    con.Close();

                }
                else MessageBox.Show("Please select a room number!");
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
            roomChackingForm rc = new roomChackingForm();
            rc.GetGuestID(guestid);
            rc.GetRoomType(type);
            rc.uname(name);
            rc.Show();
        }

        private void checkBoxcheckOutdateSame_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxcheckOutdateSame.Checked)
            {
                pictureBox8.Visible = true;
                date = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
                dateTimePicker2.Enabled = false;
                checkBox1.Enabled = false;
            }
            else
            {
                pictureBox8.Visible = false;
                dateTimePicker2.Enabled = true;
                checkBox1.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            

            if (checkBox1.Checked)
            {
                pictureBox5.Visible = true;
                checkBoxcheckOutdateSame.Enabled = false;
            }
            else
            {
                pictureBox5.Visible = false;
                checkBoxcheckOutdateSame.Enabled = true;
            }
        }

        private void checkBoxFood_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (type == "Manager")
            {
                ManagerForm form = new ManagerForm(type, name);
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
            Application.Exit();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Orange;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gold;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
