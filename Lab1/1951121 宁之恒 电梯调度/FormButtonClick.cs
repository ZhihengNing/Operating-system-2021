using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elevator_new
{
    public partial class Form1 : Form
    {
        private void butreport_click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.White)
                ((Button)sender).BackColor = Color.Red;
            else
                ((Button)sender).BackColor = Color.White;
        }
        private void butopen_click(object sender, EventArgs e)
        {
            int X = (((Button)sender).Location.X - 60) / 150;
            picbox[ElevatorContrl.elevator[X].CurrentFloorNum, X].Image = Properties.Resources.电梯开门;
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(1500) > DateTime.Now)
            {
                Application.DoEvents();
            }
            picbox[ElevatorContrl.elevator[X].CurrentFloorNum, X].Image = Properties.Resources.电梯关门;
        }

        private void butclose_click(object sender, EventArgs e)
        {
            int X = (((Button)sender).Location.X - 60) / 150;
            picbox[ElevatorContrl.elevator[X].CurrentFloorNum, X].Image = Properties.Resources.电梯关门;
        }

        private void butright_Click(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            int StatusFlag;

            if ((((Button)sender).Location.X - 820) / 60 == 0)
                StatusFlag = 1;
            else
                StatusFlag = -1;
            int ElevatorNumber = ElevatorContrl.RightButtonSet(StatusFlag, Y);//点击就添加
            //elevatorNumber表示添加数字到哪个电梯的编号
            if (ElevatorNumber == -1)
                return;
            if (ElevatorContrl.flag[ElevatorNumber] == 0)
            {
                ElevatorContrl.flag[ElevatorNumber] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(ElevatorNumber, null, null);
            }
        }
        private void but_Click0(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            ElevatorContrl.elevator[0].AddButton(0, Y);//点击就添加

            //Thread th1= new Thread(new ParameterizedThreadStart(ElevatorContrl.Run))
            if (ElevatorContrl.flag[0] == 0 && Y != ElevatorContrl.elevator[0].CurrentFloorNum)
            {
                ElevatorContrl.flag[0] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(0, null, null);
            }
        }
        private void but_Click1(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            ElevatorContrl.elevator[1].AddButton(1, Y);//点击就添加
            //Thread th1= new Thread(new ParameterizedThreadStart(ElevatorContrl.Run))
            if (ElevatorContrl.flag[1] == 0 && Y != ElevatorContrl.elevator[1].CurrentFloorNum)
            {
                ElevatorContrl.flag[1] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(1, null, null);
            }
        }
        private void but_Click2(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            ElevatorContrl.elevator[2].AddButton(2, Y);//点击就添加
            //Thread th1= new Thread(new ParameterizedThreadStart(ElevatorContrl.Run))
            if (ElevatorContrl.flag[2] == 0 && Y != ElevatorContrl.elevator[2].CurrentFloorNum)
            {
                ElevatorContrl.flag[2] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(2, null, null);
            }
        }
        private void but_Click3(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            ElevatorContrl.elevator[3].AddButton(3, Y);//点击就添加
            //Thread th1= new Thread(new ParameterizedThreadStart(ElevatorContrl.Run))
            if (ElevatorContrl.flag[3] == 0 && Y != ElevatorContrl.elevator[3].CurrentFloorNum)
            {
                ElevatorContrl.flag[3] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(3, null, null);
            }
        }
        private void but_Click4(object sender, EventArgs e)
        {
            int Y = ((622 - ((Button)sender).Location.Y) / 28);
            ElevatorContrl.elevator[4].AddButton(4, Y);//点击就添加
            //Thread th1= new Thread(new ParameterizedThreadStart(ElevatorContrl.Run))
            if (ElevatorContrl.flag[4] == 0 && Y != ElevatorContrl.elevator[4].CurrentFloorNum)
            {
                ElevatorContrl.flag[4] = 1;
                Action<int> action = ElevatorContrl.Run;//多线程的操作,int,int是传入参数的类型
                action.BeginInvoke(4, null, null);
            }
        }


    }
}
