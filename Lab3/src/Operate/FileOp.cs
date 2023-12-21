using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filesystem
{
    partial class MainForm
    {
        ListView File = new ListView();
        private void SetDataGird()//初始化文件夹
        {
            File.Location = new Point(330, 30);
            File.Size = new Size(760, 500);
            File.FullRowSelect = true;
            string[] names = { "名称", "修改日期", "类型", "大小" };
            int[] Width = { 260, 205, 120, 75 };
            for (int i = 0; i < 4; i++)
                File.Columns.Add(names[i], Width[i]);   
            File.View = View.Details;
            File.SmallImageList = imageList1;
            File.DoubleClick += new EventHandler(Item_DoubleClick);
            File.MouseClick += new MouseEventHandler(File_MouseClick);
            File.MouseDown += new MouseEventHandler(File_MouseDown);
            Controls.Add(File);
        }
        public void ShowFiles(TreeNode need)
        {
            File.Items.Clear();
            for (int i = 0; i < need.Nodes.Count; i++)
            {
                TreeNode node = need.Nodes[i];
                treeNode temp = new treeNode((treeNode)(node.Tag));
                ListViewItem a = new ListViewItem(temp.name)
                {
                    ImageIndex = node.ImageIndex
                };
                a.SubItems.Add(temp.time);
                a.SubItems.Add(temp.type);
                a.SubItems.Add(temp.size);
                File.Items.Add(a);
            }
        }
        private void File_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (File.SelectedItems.Count > 0)
                {
                    contextMenuStrip1.Show(File, new Point(e.X, e.Y));
                }
            }
        }
        private void File_MouseDown(object sender, MouseEventArgs e)
        {
            if (File.HitTest(e.X, e.Y).Item == null&&e.Button==MouseButtons.Right) //点击空白处
            {
                contextMenuStrip2.Show(File, new Point(e.X, e.Y));
            }
        }
    }
}
