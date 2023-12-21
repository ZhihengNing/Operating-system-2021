using System.Windows.Forms;

namespace Elevator_new
{
    public partial class Form1 : Form
    {
        public static int StopTime;
        public static int OpenTime = 1000;
        public static int CloseTime = 1000;
        public static Button[,] but = new Button[ElevatorContrl.ElevatorHeight, ElevatorContrl.ElevatorNumber];
        public static PictureBox[,] picbox = new PictureBox[ElevatorContrl.ElevatorHeight, ElevatorContrl.ElevatorNumber];
        public static PictureBox[] FloorNumberpic = new PictureBox[ElevatorContrl.ElevatorNumber];
        public static Button[] down = new Button[ElevatorContrl.ElevatorHeight];
        public static Button[] up = new Button[ElevatorContrl.ElevatorHeight];
        private static Button[] opendoor = new Button[ElevatorContrl.ElevatorNumber];
        private static Button[] closedoor = new Button[ElevatorContrl.ElevatorNumber];
        private static Button[] reportdoor = new Button[ElevatorContrl.ElevatorNumber];

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 4;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ProduceButton();
            ProducrFloorNumberPic();
            ProduceButtonRight(up, 750, 0);
            ProduceButtonRight(down, 750, 1);
            Produceopendoor();
            Produceclosedoor();
            ProduceReport();
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
                DrawFloorNumber(1, i);
        }
    }
}
