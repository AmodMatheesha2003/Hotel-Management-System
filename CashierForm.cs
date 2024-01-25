using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace HotelSystem
{
    public partial class CashierForm : Form
    {
        string text;
        public CashierForm(string name)
        {
            InitializeComponent();
            this.name = name;
        }

        
        String name;
        DataColumn checkBoxColumn;
        DataSet ds;
        String food, bar, roommType, isPoolAvaiilable;
        TimeSpan days;
        int childCount, adultCount, totalBill = 0, totalDays;
        Dictionary<string, int> prices = new Dictionary<string, int>();
        int count = 0;
        DataTable dt;
        String guestId = null;
        List<int> roomNumbers = new List<int>();
        List<int> indexNumbers = new List<int>();

        private void button2_Click(object sender, EventArgs e)
        {
            totalBill = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                roomNumbers.Clear(); //
                int roomNum;
                //string connectionString = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
                string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";

                if (checkBox1.Checked && dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        roomNum = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        roomNumbers.Add(roomNum);

                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();

                        String sql = "SELECT roomType, IsPoolAvailable FROM tblRoomStatus WHERE roomNumber=@number";
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@number", roomNum);

                        SqlDataReader dr = cmd.ExecuteReader();
                        dr.Read();

                        roommType = dr.GetValue(0).ToString();
                        isPoolAvaiilable = dr.GetValue(1).ToString();

                        connection.Close();

                        food = ds.Tables[0].Rows[i][2].ToString();
                        bar = ds.Tables[0].Rows[i][3].ToString();
                        days = DateTime.Parse(ds.Tables[0].Rows[i][5].ToString()) - DateTime.Parse(ds.Tables[0].Rows[i][4].ToString());
                        totalDays = days.Days;
                        childCount = Convert.ToInt32(ds.Tables[0].Rows[i][6]);
                        adultCount = Convert.ToInt32(ds.Tables[0].Rows[i][7]);

                        CalculateBill();
                        cmd.Parameters.Clear();
                    }

                    textBox1.Text = totalBill.ToString() + " LKR";
                }
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        //bool isCheckBoxChecked = (bool)dataGridView1.Rows[0].Cells["CheckBoxColumn"].Value;
                        if ((bool)dataGridView1.Rows[i].Cells["CheckBoxColumn"].Value == true)
                        {
                            //roomNum = Convert.ToInt32(dataGridView1.Rows[i].Cells[0]);
                            roomNum = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                            roomNumbers.Add(roomNum);

                            SqlConnection connection = new SqlConnection(connectionString);
                            connection.Open();

                            String sql = "SELECT roomType, IsPoolAvailable FROM tblRoomStatus WHERE roomNumber=@number";
                            SqlCommand cmd = new SqlCommand(sql, connection);
                            cmd.Parameters.AddWithValue("@number", roomNum);

                            SqlDataReader dr = cmd.ExecuteReader();
                            dr.Read();

                            roommType = dr.GetValue(0).ToString();
                            isPoolAvaiilable = dr.GetValue(1).ToString();

                            connection.Close();

                            food = ds.Tables[0].Rows[i][2].ToString();
                            bar = ds.Tables[0].Rows[i][3].ToString();
                            days = DateTime.Parse(ds.Tables[0].Rows[i][5].ToString()) - DateTime.Parse(ds.Tables[0].Rows[i][4].ToString());
                            totalDays = days.Days;
                            childCount = Convert.ToInt32(ds.Tables[0].Rows[i][6]);
                            adultCount = Convert.ToInt32(ds.Tables[0].Rows[i][7]);

                            CalculateBill();
                            cmd.Parameters.Clear();
                        }
                        /*else
                        {
                            indexNumbers.Add(i);
                        }*/
                    }

                    if (totalBill != 0) textBox1.Text = totalBill.ToString() + " LKR";
                    else textBox1.Text = string.Empty;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            string cs = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection con = new SqlConnection(cs);

            if (textBox1.Text != "")
            {
                String checkOutRooms = "";
                groupBox1.Visible = true;
                //label3.Text = name;   new
                for (int i = 0; i < roomNumbers.Count; i++)
                {
                    checkOutRooms += $"{roomNumbers[i]}  ";
                }

                label5.Text = checkOutRooms;
                label8.Text = guestId.ToString();
                label9.Text = guestName.ToString();
                label5.Text = $"LKR {totalBill}.00";
                label6.Text = checkOutRooms;
                label7.Text = name;

                textBox1.Text = string.Empty;
                checkBox1.Visible = false;

                if (checkBox1.Checked)
                {
                    con.Open();

                    //String sql = "DELETE FROM tblBookedRooms WHERE customerID=@id";
                    String sql = "UPDATE tblBookedRooms SET checkedOut='yes' WHERE roomNumber=@room"; //update
                    SqlCommand cmd = new SqlCommand(sql, con);
                    //cmd.Parameters.AddWithValue("@id", guestId);


                    for(int i = 0; i < roomNumbers.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@room", roomNumbers[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    //cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    sql = "UPDATE tblRoomStatus SET availability='to be cleaned' WHERE roomNumber=@room";
                    cmd = new SqlCommand(sql, con);

                    for (int i = 0; i < roomNumbers.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@room", roomNumbers[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    dataGridView1.DataSource = null;
                    ds.Clear();
                    con.Close();
                }
                else
                {
                    //string cs = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
                    con.Open();

                    //String sql = "DELETE FROM tblBookedRooms WHERE roomNumber=@room";
                    String sql = "UPDATE tblBookedRooms SET checkedOut='yes' WHERE roomNumber=@room"; //update
                    SqlCommand cmd = new SqlCommand(sql, con);

                    for (int i = 0; i < roomNumbers.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@room", roomNumbers[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    con.Close();

                    con.Open();

                    sql = "UPDATE tblRoomStatus SET availability='to be cleaned' WHERE roomNumber=@room";
                    cmd = new SqlCommand(sql, con);

                    for (int i = 0; i < roomNumbers.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@room", roomNumbers[i]);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    con.Close();

                    con.Open();
                    sql = "SELECT * FROM tblBookedRooms WHERE checkedOut='no' and customerID=@guestid"; //update
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@guestid", guestId);

                    SqlDataAdapter dap = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    dap.Fill(ds);

                    dt.Rows.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dt.Rows.Add(ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5]);
                    }

                    //if (dt.Rows.Count > 0) dataGridView1.DataSource = dt;
                    if (dataGridView1.Rows.Count > 0) dataGridView1.DataSource = dt;
                    else dataGridView1.DataSource = null;
                }

                MessageBox.Show("Payment successfull", "Checked out", MessageBoxButtons.OK);
            }
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

        String guestName;

        private void CashierForm_MouseClick(object sender, MouseEventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[4].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[4].Value = false;
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            LoginForm form1 = new LoginForm();
            form1.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        string text2;
        int len = 0;

        private void CashierForm_Load(object sender, EventArgs e)
        {
            label27.Text =name; //////
            userdetail();
            groupBox4.Visible = false;

            //label14.Text = name.ToString();
            text = "Welcome, " + name.ToString() + "!";
            label14.Text = " ";
            timer1.Start();


            groupBox2.Visible = false;
            textBox1.ReadOnly = true;
            checkBox1.Visible = false;
            prices.Add("singleRoom", 10000);
            prices.Add("singleRoomWithPool", 18000);
            prices.Add("doubleRoom", 25000);
            prices.Add("doubleRoomWithPool", 33000);
            prices.Add("foodChild", 5000);
            prices.Add("foodAdult", 8000);
            prices.Add("barAdult", 8000);
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

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            textBox1.Text = string.Empty;
            totalBill = 0;

            if (count == 0)
            {
                dt = new DataTable();
                dt.Columns.Add("roomNumber");
                dt.Columns.Add("guestID");
                //dt.Columns.Add("food");
                //dt.Columns.Add("bar");
                dt.Columns.Add("checkInDate");
                dt.Columns.Add("checkOutDate");
                //dt.Columns.Add("childCount");
                //dt.Columns.Add("adultCount");

                checkBoxColumn = new DataColumn("CheckBoxColumn", typeof(bool));
                checkBoxColumn.DefaultValue = false;
                dt.Columns.Add(checkBoxColumn);

                count = 1;
            }

            dt.Rows.Clear();

            //string connectionString = "Data Source=HIRUNA;Initial Catalog=HM_System;Integrated Security=True";
            string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=work;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            String sql = "SELECT guestid, Name FROM Table3 WHERE NIC=@nic";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@nic", textBoxNIC.Text);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                guestId = dr.GetValue(0).ToString();
                guestName = dr.GetValue(1).ToString();
            }

            connection.Close();

            if (guestId != null)
            {
                connection.Open();

                sql = "SELECT * FROM tblBookedRooms WHERE customerID=@id and checkedOut='no'"; //update
                cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@id", guestId);

                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                ds = new DataSet();
                dap.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //dt.Rows.Add(ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7]);
                    dt.Rows.Add(ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5]);
                }

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = null;
                if (dt.Rows.Count > 0) dataGridView1.DataSource = dt;
                if (dataGridView1.Rows.Count <= 1) checkBox1.Visible = false;
                else checkBox1.Visible = true;

                connection.Close();
            }
            else
            {
                dataGridView1.DataSource = null;
                checkBox1.Visible = false;
            }
        }

        public void CalculateBill()
        {
            if (roommType == "double" && isPoolAvaiilable == "yes") totalBill += (prices["doubleRoomWithPool"] * totalDays);
            else if (roommType == "double") totalBill += (prices["doubleRoom"] * totalDays);
            else if (roommType == "single" && isPoolAvaiilable == "yes") totalBill += (prices["singleRoomWithPool"] * totalDays);
            else totalBill += (prices["singleRoom"] * totalDays);

            if (food == "Yes") totalBill += (((prices["foodChild"] * childCount) + (prices["foodAdult"] * adultCount)) * totalDays);

            if (bar == "Yes") totalBill += ((prices["barAdult"] * adultCount) * totalDays);
        }
    }
}
