using HZH_Controls.Controls;
using HZH_Controls.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Replace;
namespace Filesystem
{
    public partial class MainForm : Form
    {
        private List<TreeNode> PathRecord = new List<TreeNode>();
        int Record = 0;
        private TreeNode Tool;
        private int Flag = 0;
        private string zhantieType;
        private TreeNode Front;//剪切的节点
        private Dictionary<string, int> dictionnary = new Dictionary<string, int> { { "磁盘", 0 }, { "文件夹", 1 }, { "文件", 2 } };
        public MainForm()
        {
            InitializeComponent();
            Text = "文件管理";
            FilePath.Enabled = false;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            doc.Load("TreeXml.xml"); 
            RecursionTreeControl(doc.DocumentElement, Catalogue.Nodes);
            //将加载完成的XML文件显示在TreeView控件中
            粘贴ToolStripMenuItem.Enabled = false;
            SetForm();
            SetTreeView();
            SetDataGird();
            current = Catalogue.Nodes[0];
            Returnbutton.Enabled = false;
            Enterbutton.Enabled = false;
            PathRecord.Add(Catalogue.Nodes[0]);
        }
        public void SetForm()
        {
            Font = new Font("微软雅黑", 10);
            Size = new Size(1110, 575);
        }
        private void Item_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < current.Nodes.Count; i++)
            {
                string type = GetType(current.Nodes[i]);
                if (current.Nodes[i].Text == File.SelectedItems[0].Text && File.SelectedItems[0].SubItems[2].Text == type)
                {
                    current = current.Nodes[i];
                    break;
                }
            }
            if (File.SelectedItems[0].SubItems[2].Text == "文件")
            {
                ContainForm form = new ContainForm();
                form.textBox.Text = GetFileText(current);
                form.ShowDialog();
                current = current.Parent;
                ShowFiles(current);
            }
            else
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
                Catalogue.SelectedNode = current;
            }
        }
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current = Catalogue.SelectedNode;
            string name = File.SelectedItems[0].SubItems[2].Text;
            FrmInputs NewFileForm = new FrmInputs("输入",
       new string[] { "新的" + name + "名" },
       new Dictionary<string, HZH_Controls.TextInputType>() { { "新的" + name + "名", HZH_Controls.TextInputType.Regex } },
       null, null, new List<string>() { "新的" + name + "名" }
       );
            NewFileForm.ShowDialog(this);
            for (int i = 0; i < current.Nodes.Count; i++)
            {
                string type = GetType(current.Nodes[i]);
                if (current.Nodes[i].Text == File.SelectedItems[0].Text && name == type)
                {
                    current.Nodes[i].Text = NewFileForm.Values[0];
                    string filetext = GetFileText(current.Nodes[i]);
                    current.Nodes[i].Tag = new treeNode(NewFileForm.Values[0], DateTime.Now.ToString(), name, "", filetext);
                    ShowFiles(current);
                    break;
                }
            }
        }
        private void Returnbutton_Click(object sender, EventArgs e)
        {
            Record--;
            if (Record == 0)
            {
                Returnbutton.Enabled = false;
                Upbutton.Enabled = false; 
            }
            else
            {
                if (Record != PathRecord.Count - 1)
                    Enterbutton.Enabled = true;
                Returnbutton.Enabled = true;
            }
            current = PathRecord[Record];
            Catalogue.SelectedNode = current;
            ShowFiles(current);
            FilePath.Text = current.FullPath;
        }
        private void Enterbutton_Click(object sender, EventArgs e)
        {
            Record++;
            if (Record == PathRecord.Count - 1)
            {
                Enterbutton.Enabled = false;
            }
            else
            {
                if (Record != 0)
                    Returnbutton.Enabled = true;
                Enterbutton.Enabled = true;
            }
            current = PathRecord[Record];
            Catalogue.SelectedNode = current;
            ShowFiles(current);
            FilePath.Text = current.FullPath;
        }
        private void Upbutton_Click(object sender, EventArgs e)//展示目录层级
        {
            if (current.Parent != null)
            {
                for(int i=Record+1;i<PathRecord.Count;i++)
                {
                    PathRecord.RemoveAt(i);
                }
                PathRecord.Add(current.Parent);
                ShowFiles(current.Parent);
                FilePath.Text = current.Parent.FullPath;
                Catalogue.SelectedNode = Catalogue.SelectedNode.Parent;
            }
        }
        private void 新建文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFile("文件");
        }
        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFile("文件夹");
        }
        private void CreateFile(string name)
        {
            current = Catalogue.SelectedNode;
        Applier:
            {
                FrmInputs NewFileForm = new FrmInputs("输入",
           new string[] { name },
           new Dictionary<string, HZH_Controls.TextInputType>() { { name, HZH_Controls.TextInputType.Regex } },
           null, null, new List<string>() { name }
           );
                if (NewFileForm.ShowDialog(this) == DialogResult.OK)
                {
                    for (int i = 0; i < current.Nodes.Count; i++)
                    {
                        string type = new treeNode((treeNode)(current.Nodes[i].Tag)).type;
                        if (NewFileForm.Values[0] == current.Nodes[i].Text && type == name)
                        {
                            FrmTips.ShowTipsError(this, "输入重名" + name + ",请重新输入");
                            goto Applier;
                        }
                    }
                    TreeNode newtreenode = new TreeNode(NewFileForm.Values[0]);
                    newtreenode.ImageIndex = dictionnary[name];
                    if (name == "文件夹")
                        newtreenode.Tag = new treeNode(NewFileForm.Values[0], DateTime.Now.ToString(), name, "", "");
                    else if (name == "文件")
                        newtreenode.Tag = new treeNode(NewFileForm.Values[0], DateTime.Now.ToString(), name, "0KB", "");
                    Catalogue.SelectedNode.Nodes.Add(newtreenode);
                }
            }
            ShowFiles(current);
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FrmDialog.ShowDialog(this, "确认是否删除？", "删除", true) == DialogResult.OK)
            {
                current = Catalogue.SelectedNode;
                for (int i = 0; i < current.Nodes.Count; i++)
                {
                    string type1 = GetType(current.Nodes[i]);
                    if (current.Nodes[i].Text == File.SelectedItems[0].Text&&type1==File.SelectedItems[0].SubItems[2].Text)
                    {
                        current.Nodes[i].Remove();
                        break;
                    }
                }
                ShowFiles(current);
            }
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current = Catalogue.SelectedNode;
            for (int i = 0; i < current.Nodes.Count; i++)
            {
                string type1 = GetType(current.Nodes[i]);
                if (current.Nodes[i].Text == File.SelectedItems[0].Text && File.SelectedItems[0].SubItems[2].Text == type1)
                {
                    Front = current.Nodes[i];
                    Tool = (TreeNode)current.Nodes[i].Clone();
                    Flag = 1;
                    zhantieType = "剪切";
                    粘贴ToolStripMenuItem.Enabled = true;
                    return;
                }
            }
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current = Catalogue.SelectedNode;
            for (int i = 0; i < current.Nodes.Count; i++)
            {
                string type1 = GetType(current.Nodes[i]);
                if (current.Nodes[i].Text == File.SelectedItems[0].Text&&File.SelectedItems[0].SubItems[2].Text==type1)
                {
                    Tool = (TreeNode)(current.Nodes[i].Clone());
                    Flag = 1;
                    zhantieType = "复制";
                    粘贴ToolStripMenuItem.Enabled = true;
                    return;
                }
            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current = Catalogue.SelectedNode;
            if (Flag == 1)
            {
                string type = GetType(Tool);
                string size = GetSize(Tool);
                string text = new treeNode((treeNode)(Tool.Tag)).filetext;
                Tool.Tag = new treeNode(Tool.Text, DateTime.Now.ToString(), type, size, text);
                if (zhantieType == "剪切")
                {
                    Front.Remove();
                }
                for(int i=0;i<current.Nodes.Count;i++)
                {
                    string type1 = GetType(current.Nodes[i]);
                    string type2 = GetType(Tool);
                    if (current.Nodes[i].Text == Tool.Text && type1 == type2)
                    {
                        if (FrmDialog.ShowDialog(this, "目标已包含一个名为\"" + Tool.Text + "\"的文件,确认是否替换？", "替换或跳过文件", true) == DialogResult.OK)
                        {
                            current.Nodes[i].Remove();
                            current.Nodes.Add(Tool);
                        }
                        Flag = 0;
                        粘贴ToolStripMenuItem.Enabled = false;
                        ShowFiles(current);
                        return;
                    }
                }
                current.Nodes.Add(Tool);
                Flag = 0;
                粘贴ToolStripMenuItem.Enabled = false;
                ShowFiles(current);
            }
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Item_DoubleClick(sender, e);
        }
        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TreeNode temp = Catalogue.Nodes[0];
            if (FrmDialog.ShowDialog(this, "确认是否格式化？", "格式化", true) ==DialogResult.OK)
            {
                int index = temp.Nodes.Count;
                for (int i = 0; i < index; i++)
                {
                    temp.Nodes[0].Remove();
                }
            }
            ShowFiles(temp);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息WM_ERASEBKGND
                return;
            base.WndProc(ref m);
        }
    }
}
