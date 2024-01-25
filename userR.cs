using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem
{
    public partial class userR : Form
    {
        public userR()
        {
            InitializeComponent();
        }

        private void userR_Load(object sender, EventArgs e)
        {
            this.crystalReportViewer1.ReportSource = @"D:\finall\New folder finall\New folder finall\New folder\HotelSystem\userReport2.rpt";
        }
    }
}
