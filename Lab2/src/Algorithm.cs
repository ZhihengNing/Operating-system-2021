using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    class Algorithm
    {
        public static void LRUalgorithm()
        {         
            for(int i=0;i<320;i++)
            {
                if (i +5 < 320)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Form1.queue[j].Text = Use.buf[i + j].ToString();
                    }
                }
                int minindex = 0;
                for (int k = 0; k < 4; k++)
                {
                    if (Use.container[1, k] < Use.container[1, minindex])
                    {
                        minindex = k;
                    }
                }
                int temp = (Use.buf[i] - Use.buf[i] % 10) / 10;
                int judge = Contain(temp, i);
                if (judge != -1)
                    Form1.arr[Use.buf[i] % 10, judge].BackColor = Color.Pink;
                else
                    Form1.arr[Use.buf[i] % 10, minindex].BackColor = Color.Pink;
                if (judge == -1)//缺页中断
                {
                    Form1.interrupt.Text = "调出第"+Form1.Record[minindex,1].Text+"页,调入第" + temp + "页";
                    Use.container[0, minindex] = temp;
                    Use.container[1, minindex] = i;
                    Form1.ChangeNum();
                    Use.InterruptCount++;
                }
                else
                {
                    Form1.interrupt.Text = "第" + Use.buf[i] + "条指令在内存中,无需调度";
                }
                DateTime current = DateTime.Now;
                while (current.AddMilliseconds(Use.StopTime) > DateTime.Now)
                {
                    Application.DoEvents();
                }
                if (judge != -1)
                    Form1.arr[Use.buf[i] % 10, judge].BackColor = Color.AntiqueWhite;
                else
                    Form1.arr[Use.buf[i] % 10, minindex].BackColor = Color.AntiqueWhite;
               
            }
        }
        public static void FIFOalgorithm()
        {
            int index = 0;
            for(int i=0;i<320;i++)
            {
                if (i + 5 < 320)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Form1.queue[j].Text = Use.buf[i + j].ToString();
                    }
                }
                int temp = (Use.buf[i] - Use.buf[i] % 10) / 10;
                int judge = Contain(temp, i);
                if (judge != -1)
                    Form1.arr[Use.buf[i] % 10, judge].BackColor = Color.Pink;
                else
                    Form1.arr[Use.buf[i] % 10, index].BackColor = Color.Pink;
                if (judge == -1)//缺页中断
                {
                    Form1.interrupt.Text = "调出第" + Form1.Record[index, 1].Text + "页,调入第" + temp + "页";
                    Use.container[0, index] = temp;
                    Use.container[1, index] = i;
                    Form1.ChangeNum();
                    Use.InterruptCount++;
                }
                else
                {
                    Form1.interrupt.Text = "第" + Use.buf[i] + "条指令在内存中,无需调度";
                }
                DateTime current = DateTime.Now;
                while (current.AddMilliseconds(Use.StopTime) > DateTime.Now)
                {
                    Application.DoEvents();
                }
                if (judge != -1)
                    Form1.arr[Use.buf[i] % 10, judge].BackColor = Color.AntiqueWhite;
                else
                {
                    Form1.arr[Use.buf[i] % 10, index].BackColor = Color.AntiqueWhite;
                    index = (index + 1) % 4;
                }             
            }
        }
        private static int Contain(int Number,int Index)
        {
            for(int i=0;i<4;i++)
            {
                if (Use.container[0, i] == Number)
                {
                    Use.container[1, i] = Index;
                    return i;
                }
            }
            return -1; 
        }
    }
}
