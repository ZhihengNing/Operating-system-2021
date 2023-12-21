using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Elevator_new
{
    struct TwoStatus
    {
        public int Distance;
        public int ElevatorNumber;
        public int flag;
        public TwoStatus(int X, int Y,int Z)
        {
            Distance = X;
            ElevatorNumber = Y;
            flag = Z;

        }

    }

    static class ElevatorContrl//用来表示一些全局变量和函数
    {
        public static int ElevatorHeight = 20;//电梯的高度
        public static int ElevatorNumber = 5;//电梯的个数
        public static Elevator[] elevator = new Elevator[ElevatorNumber];//五部电梯
        public static int[] Upelevator = new int[ElevatorNumber];//右边的上行按钮
        public static int[] Downelevator = new int[ElevatorNumber];//右边的下行按钮
        public static int[] flag;//用来标记每部电梯是否是第一次按下
        public static List<TwoStatus> Record = new List<TwoStatus>();//用来存适合调度的电梯
        public static void Set()//初始化一些全局变量
        {
            flag = new int[ElevatorNumber];
            for (int i = 0; i < ElevatorNumber; i++)
            {
                elevator[i] = new Elevator();
                flag[i] = 0;
                Upelevator[i] = new int();
                Downelevator[i] = new int();
            }
        }
        public static void Run(int X)//运行函数
        {
            while (!(elevator[X].UpButtons.Count == 0 && elevator[X].DownButtons.Count == 0))
            {
                if (elevator[X].ElevatorStatus == 1)
                {
                    for (int i = elevator[X].CurrentFloorNum; i < ElevatorHeight; i++)
                    {
                        if (i == elevator[X].UpButtons[0])//如果到了开门的楼层就开门
                        {

                            Form1.picbox[i, X].Image = Properties.Resources.上;
                            OpenDoorStatus(i, X, elevator[X].UpButtons, 1);
                            if (elevator[X].UpButtons.Count == 0)
                            {
                                if (elevator[X].DownButtons.Count != 0)
                                    elevator[X].ElevatorStatus = -1;
                                break;
                            }
                        }
                        else
                        {
                            Form1.picbox[i, X].Image = Properties.Resources.上;
                            NormalStatus(i, X);
                        }
                        Form1.picbox[i, X].Image = null;
                    }
                }

                if (elevator[X].ElevatorStatus == -1)
                {
                    for (int i = elevator[X].CurrentFloorNum; i >= 0; i--)
                    {
                        if (i == elevator[X].DownButtons[0])//获取首数字
                        {

                            Form1.picbox[i, X].Image = Properties.Resources.下;
                            OpenDoorStatus(i, X, elevator[X].DownButtons, -1);
                            if (elevator[X].DownButtons.Count == 0)
                            {
                                if (elevator[X].UpButtons.Count != 0)
                                    elevator[X].ElevatorStatus = 1;
                                break;
                            }
                        }
                        else
                        {
                            Form1.picbox[i, X].Image = Properties.Resources.下;
                            NormalStatus(i, X);
                        }

                        Form1.picbox[i, X].Image = null;
                    }

                }
            }
            elevator[X].ElevatorStatus = 0;
            flag[X] = 0;
        }
        private static void OpenDoorStatus(int Y, int X, List<int> Buttons, int elevatorstatus)//开门状态显示
        {

            Buttons.RemoveAt(0);
            elevator[X].CurrentFloorNum = Y;
            Form1.DrawFloorNumber(Y + 1, X);//Y+1
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(Form1.StopTime) > DateTime.Now)
            {
                Application.DoEvents();
            }
            Form1.picbox[Y, X].Image = Properties.Resources.电梯开门;
            Form1.but[Y, X].FlatAppearance.BorderColor = Color.Gray;
            if (elevatorstatus == -1)
                Form1.down[Y].FlatAppearance.BorderColor = Color.Gray;
            else if (elevatorstatus == 1)
                Form1.up[Y].FlatAppearance.BorderColor = Color.Gray;
            DateTime current1 = DateTime.Now;
            while (current1.AddMilliseconds(Form1.OpenTime) > DateTime.Now)
            {
                Application.DoEvents();
            }

            Form1.picbox[Y, X].Image = Properties.Resources.电梯关门;
            DateTime current2 = DateTime.Now;
            while (current2.AddMilliseconds(Form1.CloseTime) > DateTime.Now)
            {
                Application.DoEvents();
            }
            if (Buttons.Count == 0)
            {
                return;
            }
        }
        private static void NormalStatus(int Y, int X)//正常状态图显示
        {

            Form1.DrawFloorNumber(Y + 1, X);
            elevator[X].CurrentFloorNum = Y;
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(Form1.StopTime) > DateTime.Now)
            {
                Application.DoEvents();
            }
            // elevator[X].CurrentFloorNum = Y;
        }

        public static int RightButtonSet(int StatusFlag, int Y)//statusFlag代表的是点击上行还是下行
        {
            if (StatusFlag == 1)
            {
                for (int i = 0; i < ElevatorNumber; i++)
                {
                    //所在楼层数要小于要去的楼层
                    if ((elevator[i].ElevatorStatus == StatusFlag && Y > elevator[i].CurrentFloorNum) || elevator[i].ElevatorStatus == 0)//都是向上或者静止
                    {
                        Record.Add(new TwoStatus(Math.Abs(Y - elevator[i].CurrentFloorNum), i,0));
                    }
                    else if (Y < elevator[i].CurrentFloorNum || (elevator[i].ElevatorStatus != StatusFlag && Y > elevator[i].CurrentFloorNum))
                    {
                        Record.Add(new TwoStatus(Math.Abs(40 - Y - elevator[i].CurrentFloorNum), i,1));
                    }

                }
                Record.Sort((a, b) =>
                {
                    var o = a.Distance - b.Distance;
                    return o;
                });
                int need = Record[0].ElevatorNumber;
                if (Y < elevator[Record[0].ElevatorNumber].CurrentFloorNum)
                {
                    if (!elevator[Record[0].ElevatorNumber].DownButtons.Contains(Y))
                    {
                        elevator[Record[0].ElevatorNumber].DownButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].DownButtons.Sort((x, y) => -x.CompareTo(y));
                        if (Record[0].flag==0)
                        elevator[need].ElevatorStatus = -1;
                    }
                    else if (!elevator[Record[0].ElevatorNumber].UpButtons.Contains(Y) && Record[0].flag == 1)
                    {
                        elevator[Record[0].ElevatorNumber].UpButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].UpButtons.Sort();
                    }
                }
                else
                {
                    if (!elevator[Record[0].ElevatorNumber].UpButtons.Contains(Y))
                    {

                        elevator[Record[0].ElevatorNumber].UpButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].UpButtons.Sort();
                        if (Record[0].flag == 0)
                            elevator[need].ElevatorStatus = 1;
                    }
                    else if (!elevator[Record[0].ElevatorNumber].DownButtons.Contains(Y) && Record[0].flag == 1)
                    {
                        elevator[Record[0].ElevatorNumber].DownButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].DownButtons.Sort((x, y) => -x.CompareTo(y));
                    }

                }
                Record.Clear();
                return need;
            }
            else
            {
                for (int i = 0; i < ElevatorNumber; i++)
                {
                    if (elevator[i].ElevatorStatus == StatusFlag && Y < elevator[i].CurrentFloorNum || elevator[i].ElevatorStatus == 0)//都是向上或者静止
                    {
                        Record.Add(new TwoStatus(Math.Abs(Y - elevator[i].CurrentFloorNum), i, 0));
                    }
                    else if (Y > elevator[i].CurrentFloorNum || (elevator[i].ElevatorStatus != StatusFlag && Y < elevator[i].CurrentFloorNum))
                    {
                        Record.Add(new TwoStatus(Math.Abs(40 - Y - elevator[i].CurrentFloorNum), i, 1));
                    }
                }
                Record.Sort((a, b) =>
                {
                    var o = a.Distance - b.Distance;
                    return o;
                });
                int need = Record[0].ElevatorNumber;
                if (Y > elevator[Record[0].ElevatorNumber].CurrentFloorNum)
                {
                    if (!elevator[Record[0].ElevatorNumber].UpButtons.Contains(Y) )
                    {
                        elevator[Record[0].ElevatorNumber].UpButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].UpButtons.Sort();
                        if(Record[0].flag == 0)
                        elevator[need].ElevatorStatus = 1;
                    }
                    else if (!elevator[Record[0].ElevatorNumber].DownButtons.Contains(Y) && Record[0].flag == 1)
                    {
                        elevator[Record[0].ElevatorNumber].DownButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].DownButtons.Sort((x, y) => -x.CompareTo(y));
                    }
                }
                else
                {
                    if (!elevator[Record[0].ElevatorNumber].DownButtons.Contains(Y))
                    {

                        elevator[Record[0].ElevatorNumber].DownButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].DownButtons.Sort((x, y) => -x.CompareTo(y));
                        if (Record[0].flag == 0)
                            elevator[need].ElevatorStatus = -1;
                    }
                    else if(!elevator[Record[0].ElevatorNumber].UpButtons.Contains(Y) && Record[0].flag == 1)
                    {
                        elevator[Record[0].ElevatorNumber].UpButtons.Add(Y);
                        elevator[Record[0].ElevatorNumber].UpButtons.Sort();
                    }
                    
                }
                Record.Clear();
               
                return need;
            }
        }
    }


}
