using System;
using System.Windows.Forms;
namespace Elevator_new
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form2 FrontSelect = new Form2();
            Application.Run(FrontSelect);
            ElevatorContrl.ElevatorNumber = FrontSelect.comboBox1.SelectedIndex + 1;
            ElevatorContrl.ElevatorHeight = 20 - FrontSelect.comboBox2.SelectedIndex;
            ElevatorContrl.Set();
            Application.Run(new Form1());
        }
    }
}
