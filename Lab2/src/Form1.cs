using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class Form1 : Form
    {
        public static Button[,] arr = new Button[10, 4];
        private static Button but = new Button();
        private Label[] Uplabels = new Label[4];
        private static Label[] Downlabels = new Label[4];
        public static Button ClearData = new Button();
        public static Button[,] Record = new Button[4, 2];
        public static Label interrupt = new Label();
        private TrackBar Speed = new TrackBar();
        public static Button[] queue = new Button[6];
        private Button SelectLRU = new Button();
        private Button SelectFIFO = new Button();
        private int flag = 0;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(880, 600);
            this.Text = "操作系统--内存管理项目";
            SetButton();
            SetLables();
            SetTrackBar();
        }
        public void SetTrackBar()
        {
            Speed.Maximum = 195;
            Speed.Value = 100;
            Controls.Add(Speed);
            Speed.Location = new Point(700, 230);
            Speed.Size = new Size(120, 20);
            Speed.Scroll += new EventHandler(trackBar1_Scroll);
        }
        public void SetButton()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr[i, j] = new Button();
                    arr[i, j].Text = "NULL";
                    arr[i, j].Font = new Font("times new roman", 13);
                    arr[i, j].Size = new Size(Use.ButtonLength, Use.ButtonHeight);
                    arr[i, j].Location = new Point(j * 130 + Use.StartX, i * 40 + Use.StartY);
                    arr[i, j].FlatStyle = FlatStyle.Flat;
                    arr[i, j].BackColor = Color.AntiqueWhite;
                    arr[i, j].FlatAppearance.BorderColor = Color.DarkGray;
                    arr[i, j].Enabled = false;
                    Controls.Add(arr[i, j]);
                }
            }
            Controls.Add(but);
            but.Text = "开始模拟";
            but.Font = new Font("楷体", 12);
            but.FlatStyle = FlatStyle.Flat;
            but.FlatAppearance.BorderColor = Color.White;
            but.Size = new Size(100, 40);
            but.Location = new Point(580, 300);
            but.Click += new EventHandler(ButClick);
            Controls.Add(ClearData);
            ClearData.Text = "清空数据";
            ClearData.Font = new Font("楷体", 12);
            ClearData.Size = new Size(100, 40);
            ClearData.FlatStyle = FlatStyle.Flat;
            ClearData.FlatAppearance.BorderColor = Color.White;
            ClearData.Location = new Point(730, 300);
            ClearData.Click += new EventHandler(ClearDataClick);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Record[i, j] = new Button();
                    Record[i, j].FlatStyle = FlatStyle.Flat;
                    Record[i, j].Font = new Font("times new roman", 12);
                    Record[i, j].BackColor = Color.AntiqueWhite;
                    Record[i, j].Size = new Size(60, 40);
                    Record[i, j].Location = new Point(j * 60 + 550, i * 40 + 100);
                    Controls.Add(Record[i, j]);
                }
                Record[i, 0].Text = i.ToString();
                Record[i, 1].Text = "NULL";
            }
            for (int i = 0; i < 6; i++)
            {
                queue[i] = new Button();
                queue[i].Size = new Size(50, 30);
                queue[i].FlatStyle = FlatStyle.Flat;
                queue[i].Location = new Point(560 + i * 50, 390);
                Controls.Add(queue[i]);
            }
            SelectLRU.Location = new Point(680, 100);
            SelectLRU.Size = new Size(180, 40);
            SelectLRU.FlatStyle = FlatStyle.Flat;
            SelectLRU.BackColor = Color.Pink;
            SelectLRU.Text = "LRU-最近最少使用算法";
            SelectLRU.Font = new Font("楷体", 12);
            SelectLRU.Click += new EventHandler(SelectLRUClick);
            SelectFIFO.FlatStyle = FlatStyle.Flat;
            SelectFIFO.FlatAppearance.BorderColor = Color.CornflowerBlue;
            SelectLRU.FlatAppearance.BorderColor = Color.CornflowerBlue;
            SelectFIFO.Location = new Point(680, 140);
            SelectFIFO.Size = new Size(180, 40);
            SelectFIFO.Text = "FIFO-先进先出算法";
            SelectFIFO.Font = new Font("楷体", 12);
            SelectFIFO.Click += new EventHandler(SelectFIFOClick);
            Controls.Add(SelectFIFO);
            Controls.Add(SelectLRU);
        }
        private void SelectLRUClick(object sender, EventArgs e)
        {
            SelectLRU.BackColor = Color.Pink;
            SelectFIFO.BackColor = Color.White;
            flag = 0;
        }
        private void SelectFIFOClick(object sender, EventArgs e)
        {
            SelectLRU.BackColor = Color.White;
            SelectFIFO.BackColor = Color.Pink;
            flag = 1;
        }
            private void SetLables()
        {
            for (int i = 0; i < 4; i++)
            {
                Uplabels[i] = new Label();
                Uplabels[i].Font = new Font("Times new roman", 15);
                Uplabels[i].Text = "物理" + i + "页";
                Uplabels[i].Location = new Point(Use.StartX + 130 * i, Use.StartY - 30);
                Uplabels[i].BackColor = Color.CornflowerBlue;
                this.Controls.Add(Uplabels[i]);
                Downlabels[i] = new Label();
                Downlabels[i].Font = new Font("Times new roman", 15);
                Downlabels[i].Text = "逻辑-1页";
                Downlabels[i].Location = new Point(Use.StartX + 130 * i, Use.StartY + 400);
                Downlabels[i].BackColor = Color.CornflowerBlue;
                this.Controls.Add(Downlabels[i]);
            }
            interrupt.Font = new Font("times new roman", 15);
            interrupt.Size = new Size(300, 30);
            interrupt.BackColor = Color.CornflowerBlue;
            interrupt.Location = new Point(570, 510);
            Controls.Add(interrupt);
        }
        private void ButClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                queue[i].Text = "";
            }
            but.Enabled = false;
            ClearData.Enabled = false;
            Use.RandomNum();
            Use.InitBuf();
            if (flag == 0)
                Algorithm.LRUalgorithm();
            else
                Algorithm.FIFOalgorithm();
            interrupt.Text = "缺页率为：" + (Use.InterruptCount / 320.0 * 100).ToString() + "%";
            Use.InterruptCount = 0;
            but.Enabled = true;
            ClearData.Enabled = true;
        }
        private void ClearDataClick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arr[i, j].Text = "NULL";
                    Record[j, 1].Text = "NULL";
                    Downlabels[j].Text = "逻辑-1页";
                }
            }
            for (int i = 0; i < 6; i++)
            {
                queue[i].Text = "";
            }
            Use.InterruptCount = 0;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.DrawString("页表信息", new Font("times new roman", 15), Brushes.Black, 560, 70);
            g.FillRectangle(Brushes.CornflowerBlue, new Rectangle(20, 50, 500, 500));
            g.FillRectangle(Brushes.CornflowerBlue, new Rectangle(550, 285, 315, 265));
            g.DrawString("  — — — —调度信息— — — —", new Font("Times New Roman", 15), Brushes.Black, 550, 470);
            g.DrawString("速度调节", new Font("Times New Roman", 12), Brushes.Black, 720, 210);
            g.DrawString("快", new Font("Times New Roman", 12), Brushes.Black, 820, 230);
            g.DrawString("慢", new Font("Times New Roman", 12), Brushes.Black, 680, 230);
            g.DrawString("等待执行的指令队列", new Font("Times New Roman", 10), Brushes.Black, 560, 370);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Use.StopTime = 1970 - Speed.Value * 10;
        }
    }
}
