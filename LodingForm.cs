using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelSystem
{
    public partial class LodingForm : Form
    {
        public LodingForm()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int x = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (x <= 100)
            {
                x += 2;
                
            }
            else
            {
                timer1.Stop();

                LoginForm form1 = new LoginForm();
                form1.Show();
                this.Hide();
            }
        }
    }
}
