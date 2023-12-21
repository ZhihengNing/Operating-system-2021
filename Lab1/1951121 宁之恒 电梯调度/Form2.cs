using System;
using System.Windows.Forms;
namespace Elevator_new
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 4;
            comboBox2.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.AcceptButton = Button1;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
