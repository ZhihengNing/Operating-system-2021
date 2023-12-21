using System.Collections.Generic;
using System.Drawing;

namespace Elevator_new
{
    class Elevator
    {
        public List<int> UpButtons = new List<int>();//上升队列
        public List<int> DownButtons = new List<int>();//下降队列
        public int ElevatorStatus;//-1代表向下，0代表暂停，1代表向上
        public int CurrentFloorNum;//代表电梯当前在几楼
        public Elevator()
        {
            ElevatorStatus = 0;
            CurrentFloorNum = 0;
        }
       
        public void AddButton(int X, int Y)//添加合适的数字进到上行数组里面，只有符合要求加入数组的按钮才变色
        {
            if (CurrentFloorNum > Y && !DownButtons.Contains(Y))
            {
                DownButtons.Add(Y);
                Form1.but[Y, X].FlatAppearance.BorderColor = Color.Red;
                DownButtons.Sort((x, y) => -x.CompareTo(y));
                if (ElevatorStatus == 0)
                    ElevatorStatus = -1;
            }
            if (CurrentFloorNum < Y && !UpButtons.Contains(Y))
            {
                UpButtons.Add(Y);
                Form1.but[Y, X].FlatAppearance.BorderColor = Color.Red;
                UpButtons.Sort();
                if (ElevatorStatus == 0)
                    ElevatorStatus = 1;
            }
        }
    }
}
    

