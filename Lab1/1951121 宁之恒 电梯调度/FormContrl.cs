using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elevator_new
{
    public partial class Form1 : Form
    {
        private void ProduceReport()
        {
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
            {
                reportdoor[i] = new Button();
                reportdoor[i].Text = "警";
                reportdoor[i].BackColor = Color.White;
                reportdoor[i].Location = new Point(10 + 150 * i, 680);
                reportdoor[i].BackColor = Color.White;
                reportdoor[i].Font = new Font("Times New Roman", 10);
                reportdoor[i].Size = new Size(42, 24);
                reportdoor[i].FlatStyle = FlatStyle.Flat;
                reportdoor[i].FlatAppearance.BorderColor = Color.Gray;
                reportdoor[i].Click += new EventHandler(butreport_click);
                this.Controls.Add(reportdoor[i]);
            }
        }
        private void Produceopendoor()
        {
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
            {
                opendoor[i] = new Button();
                opendoor[i].Text = "开";
                opendoor[i].Location = new Point(60 + 150 * i, 652);
                opendoor[i].BackColor = Color.White;
                opendoor[i].Font = new Font("Times New Roman", 10);
                opendoor[i].Size = new Size(42, 24);
                opendoor[i].FlatStyle = FlatStyle.Flat;
                opendoor[i].FlatAppearance.BorderColor = Color.Gray;
                opendoor[i].Click += new EventHandler(butopen_click);
                this.Controls.Add(opendoor[i]);
            }
        }

        private void Produceclosedoor()
        {
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
            {
                closedoor[i] = new Button();
                closedoor[i].Text = "关";
                closedoor[i].Location = new Point(60 + 150 * i, 680);
                closedoor[i].BackColor = Color.White;
                closedoor[i].Font = new Font("Times New Roman", 9);
                closedoor[i].Size = new Size(42, 24);
                closedoor[i].FlatStyle = FlatStyle.Flat;
                closedoor[i].FlatAppearance.BorderColor = Color.Gray;
                closedoor[i].Click += new EventHandler(butclose_click);
                this.Controls.Add(closedoor[i]);
            }
        }
        private void ProducrFloorNumberPic()
        {
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
            {
                FloorNumberpic[i] = new PictureBox();
                FloorNumberpic[i].Location = new Point(20 + 150 * i, 47);
                FloorNumberpic[i].Size = new Size(95, 32);
                this.Controls.Add(FloorNumberpic[i]);
            }
        }
        private void ProduceButton()
        {
            for (int i = 0; i < ElevatorContrl.ElevatorHeight; i++)
            {
                for (int j = 0; j < ElevatorContrl.ElevatorNumber; j++)
                {
                    picbox[i, j] = new PictureBox();
                    picbox[i, j].Size = new Size(32, 30);//35 24
                    picbox[i, j].Location = new Point(17 + 150 * j, 622 - 28 * i);
                    picbox[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    picbox[i, j].Name = (j * 20 + i + 1).ToString();

                    but[i, j] = new Button();
                    but[i, j].Name = (j * 20 + i + 1).ToString();
                    but[i, j].BackColor = Color.White;
                    but[i, j].Font = new Font("Times New Roman", 10);
                    but[i, j].Size = new Size(42, 24);
                    but[i, j].FlatStyle = FlatStyle.Flat;
                    but[i, j].FlatAppearance.BorderColor = Color.Gray;
                    but[i, j].Location = new Point(150 * j + 60, 622 - 28 * i);
                    but[i, j].Text = (i + 1).ToString();
                    but[i, j].FlatAppearance.BorderSize = 1;
                    this.Controls.Add(but[i, j]);
                    this.Controls.Add(picbox[i, j]);
                    if (j == 0)
                        but[i, j].Click += new EventHandler(this.but_Click0);
                    else if (j == 1)
                        but[i, j].Click += new EventHandler(this.but_Click1);
                    else if (j == 2)
                        but[i, j].Click += new EventHandler(this.but_Click2);
                    else if (j == 3)
                        but[i, j].Click += new EventHandler(this.but_Click3);
                    else
                        but[i, j].Click += new EventHandler(this.but_Click4);
                }
            }
            for (int i = 0; i < ElevatorContrl.ElevatorNumber; i++)
            {
                picbox[0, i].Image = Properties.Resources.电梯关门;
            }
        }
        private void ProduceButtonRight(Button[] but, int start_x, int number)
        {
            for (int i = 0; i < ElevatorContrl.ElevatorHeight; i++)
            {
                but[i] = new Button();
                if (number == 1)
                    but[i].Name = (i + number).ToString();
                else
                    but[i].Name = (i + 1 + number).ToString();
                but[i].BackColor = Color.White;
                if (number == 0)
                    but[i].BackgroundImage = Properties.Resources.红三角上;
                else
                    but[i].BackgroundImage = Properties.Resources.红色三角形下;
                but[i].BackgroundImageLayout = ImageLayout.Zoom;
                but[i].Size = new Size(38, 24);
                but[i].FlatStyle = FlatStyle.Flat;
                but[i].FlatAppearance.BorderColor = Color.Gray;
                but[i].Location = new Point(start_x + 70 + number * 60, 622 - 28 * i);
                this.Controls.Add(but[i]);
                //单击事件
                if (i == ElevatorContrl.ElevatorHeight - 1 && number == 0)
                {
                    but[i].Visible = false;
                    but[i].Enabled = false;
                }
                if (i == 0 && number == 1)
                {
                    but[i].Enabled = false;
                    but[i].Visible = false;
                }
                but[i].Click += new EventHandler(this.butright_Click);
            }
        }
    }
}
