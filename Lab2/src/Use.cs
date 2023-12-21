using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    static class Use
    {
        public static int StartX = 30;//起始位置X
        public static int StartY = 100;//起始位置Y
        public static int ButtonLength = 90;//按钮长度
        public static int ButtonHeight = 30;//按钮宽度
        public static int[,] container = new int[2, 4];//用来存储每个物理内存块信息
        public static int[] buf = new int[320];//用来存储随机数
        public static int InterruptCount = 0;//缺页次数
        public static int StopTime = 1000;//停滞时间
        public static void RandomNum()//初始化数组buf
        {
            Random ra = new Random();
            for (int i=0;i<320;i+=4)
            {
                int temp = ra.Next(0, 318);//防止temp2+1=320
                int temp1 = ra.Next(0, temp);
                buf[i] = temp1;
                buf[i + 1] = temp1 + 1;
                int temp2 = ra.Next(temp, 319);
                buf[i + 2] = temp2;
                buf[i + 3] = temp2+1;
            }
        }
        public static void InitBuf()//初始化队列数组
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    container[i, j] = -1;
                }
            }
        }
    }
}
