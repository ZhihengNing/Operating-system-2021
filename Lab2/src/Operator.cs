using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    partial class Form1
    {
        public static void ChangeNum()
        {          
            for(int j=0;j<4;j++)
            {
                for(int i=0;i<10;i++)
                {
                    if (Use.container[0, j] != -1)
                    {
                        arr[i, j].Text = Use.container[0, j].ToString() + i;
                    }
                }
                if (Use.container[0, j] != -1)
                {
                    Downlabels[j].Text = "逻辑" + (Use.container[0, j]).ToString() + "页";
                    Record[j, 1].Text = (Use.container[0, j]).ToString();
                }
            }
            
        }          
    }
}
