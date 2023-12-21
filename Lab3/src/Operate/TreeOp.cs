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
        private TreeView Catalogue = new TreeView();//完整的
        public static TreeNode current = new TreeNode();
        public void SetTreeView()
        {
            Controls.Add(Catalogue);
            Catalogue.ImageList = imageList1;
            Catalogue.Font = new Font("微软雅黑", 10);
            Catalogue.Location = new Point(0, 30);
            Catalogue.Size = new Size(325, 500);
            Catalogue.AfterSelect += new TreeViewEventHandler(Catalogue_AfterSelect);
            Catalogue.MouseClick += new MouseEventHandler(Catalogue_MouseClick);
        }
        private void Catalogue_AfterSelect(object sender, TreeViewEventArgs e)  //鼠标点击节点触发的事件
        {
            string type = GetType(Catalogue.SelectedNode);
            Catalogue.SelectedImageIndex = Catalogue.SelectedNode.ImageIndex;
            current = Catalogue.SelectedNode;
            if (current.Parent == null)
                Upbutton.Enabled = false;
            else
                Upbutton.Enabled = true;
            if (type != "文件")
            {
                if (PathRecord[Record] != current)//如果不相等就加入
                {
                    for (int i = Record + 1; i < PathRecord.Count; i++)
                    {
                        PathRecord.RemoveAt(i);
                    }
                    PathRecord.Add(current);
                    Record++;
                    Returnbutton.Enabled = true;
                }
                ShowFiles(current);
                FilePath.Text = current.FullPath;
            }
            else
                ShowFiles(current.Parent);
        }
        private void Catalogue_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (File.SelectedItems.Count > 0)
                {
                    contextMenuStrip1.Show(File, new Point(e.X, e.Y));
                }
            }
        }
        private string GetType(TreeNode a)
        {
            return new treeNode((treeNode)(a.Tag)).type;
        }
        private string GetSize(TreeNode a)
        {
            return new treeNode((treeNode)(a.Tag)).size;
        }
        private string GetFileText(TreeNode a)
        {
            return new treeNode((treeNode)(a.Tag)).filetext;
        }
    
    }
}
