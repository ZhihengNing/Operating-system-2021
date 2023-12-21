using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Filesystem;
using HZH_Controls.Forms;

namespace Replace
{
    public partial class ContainForm : Form
    {
        public RichTextBox textBox = new RichTextBox();
        public ContainForm()
        {
            InitializeComponent();
            Text = "文本框";
            Size = new Size(400, 400);
            Controls.Add(textBox);
            textBox.Size = new Size(400, 400);
            textBox.Font = new Font("微软雅黑", 12);
            textBox.Location = new Point(0, 0);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            string text = new treeNode((treeNode)MainForm.current.Tag).filetext;
            if (text != textBox.Text)
            {
                if (FrmDialog.ShowDialog(this, "你想将更改保存嘛？", "记事本", true) == DialogResult.OK)
                {
                    MainForm.current.Tag = new treeNode(MainForm.current.Text, DateTime.Now.ToString(), "文件", Math.Ceiling(textBox.Text.Length/1000.0)+"KB", textBox.Text);
                }
            }
        }
    }
}
