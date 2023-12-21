using System.Drawing;
using System.Windows.Forms;

namespace Elevator_new
{
    public partial class Form1 : Form
    {
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            StopTime = 1100 - (comboBox1.SelectedIndex + 1) * 100;
            Graphics g = this.CreateGraphics();

            //出来一个画笔,这只笔画出来的颜色是红的
            for (var i = 5; i < (ElevatorContrl.ElevatorNumber - 1) * 150 + 50; i += 150)
            {
                DrawRectangle(g, i);
            }
            DrawWordElevator(g);
            DrawNumber(g);
            for (int X = 0; X < ElevatorContrl.ElevatorNumber; X++)
            {
                g.DrawString((1).ToString(), new Font("Times New Roman", 12), Brushes.Gray, 91 + X * 150, 62);
                g.DrawString(0.ToString(), new Font("Times New Roman", 22, FontStyle.Bold), Brushes.Black, 50 + X * 150, 52);
            }
        }
        public static void DrawFloorNumber(int Y, int X)//Y最多20行，X最多5列
        {
            Bitmap img = new Bitmap(95, 32);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 95, 32));
            if (Y < ElevatorContrl.ElevatorHeight)
            {
                g.DrawString((Y + 1).ToString(), new Font("Times New Roman", 12), Brushes.Gray, 71, 15);
            }
            g.DrawString((Y - 1).ToString(), new Font("Times New Roman", 12), Brushes.Gray, -2, 15);
            g.DrawString(Y.ToString(), new Font("Times New Roman", 22, FontStyle.Bold), Brushes.Black, 30, 5);
            FloorNumberpic[X].Image = img;
        }
        private void DrawWordElevator(Graphics g)//写最上面几个字"电梯X”
        {
            int number = 1;
            for (int count = 0; count <= (ElevatorContrl.ElevatorNumber - 1) * 150 + 50; count += 150)
            {
                g.DrawString("电梯" + number.ToString(), new Font("Times New Roman", 15), Brushes.Black, 36 + count, 13);
                number++;
            }
        }
        private void DrawRectangle(Graphics g, int x)//画矩形
        {
            Pen p = new Pen(Brushes.Orange, 3);
            g.DrawRectangle(p, new Rectangle(x, 10, 120, 700));
        }
        private void DrawNumber(Graphics g)//画电梯数字
        {
            for (int i = 0; i < ElevatorContrl.ElevatorHeight; i++)
            {
                if (i < 9)
                    g.DrawString("F  " + (i + 1).ToString(), new Font("Times New Roman", 12), Brushes.Black, 790, 622 - 28 * i);
                else
                    g.DrawString("F" + (i + 1).ToString(), new Font("Times New Roman", 12), Brushes.Black, 790, 622 - 28 * i);
            }
        }

    }
}
