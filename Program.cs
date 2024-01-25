using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HotelSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new CleanerForm("amod"));
            //Application.Run(new RoomEditForm("Manager", "Amod"));
            //Application.Run(new Form2("Manager", "Amod"));

           // GuestForm form6 = new GuestForm();
          // form6.GetType("Manager");
           //form6.username("Amod");
            
            //Application.Run(new GuestForm());

            Application.Run(new LodingForm());

            //Application.Run(new Form2(" "," "));
        }
    }
}
