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
    public partial class roomsReportForm : Form
    {
        public roomsReportForm()
        {
            InitializeComponent();
        }

        private void roomsReportForm_Load(object sender, EventArgs e)
        {
           this.crystalReportViewer1.ReportSource = @"D:\finall\New folder finall\New folder finall\New folder\HotelSystem\roomsReport.rpt";
        }
    }
}
