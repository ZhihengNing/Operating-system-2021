using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Filesystem
{
    partial class MainForm
    {
        XmlDocument doc = new XmlDocument();
        StringBuilder sb = new StringBuilder();
        //XML每行的内容
        private string xmlLine = "";
        /// <summary>
        /// RecursionTreeControl:表示将XML文件的内容显示在TreeView控件中
        /// </summary>
        /// <param name="xmlNode">将要加载的XML文件中的节点元素</param>
        /// <param name="nodes">将要加载的XML文件中的节点集合</param>
        private void RecursionTreeControl(XmlNode xmlNode, TreeNodeCollection nodes)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)//循环遍历当前元素的子元素集合
            {
                TreeNode new_child = new TreeNode();//定义一个TreeNode节点对象
                new_child.Name = node.Attributes["Name"].Value;
                new_child.Text = node.Attributes["Text"].Value;
                treeNode arr = new treeNode(node.Attributes["name"].Value, node.Attributes["time"].Value, node.Attributes["type"].Value, node.Attributes["size"].Value ,node.Attributes["filetext"].Value);
                new_child.Tag = arr;
                string type = new treeNode((treeNode)(new_child.Tag)).type;
                new_child.ImageIndex = dictionnary[type];
                nodes.Add(new_child);
                RecursionTreeControl(node, new_child.Nodes);
            }
        }
        //递归遍历节点内容,最关键的函数
        private void ParseNode(TreeNode tn, StringBuilder sb)
        {
            IEnumerator ie = tn.Nodes.GetEnumerator();
            while (ie.MoveNext())
            {
                TreeNode ctn = (TreeNode)ie.Current;
                xmlLine = GetRSSText(ctn);
                sb.Append(xmlLine);
                //如果还有子节点则继续遍历
                if (ctn.Nodes.Count > 0)
                {
                    ParseNode(ctn, sb);
                }
                sb.Append("</Node>\n");
            }
        }
        //成生RSS节点的XML文本行
        private string GetRSSText(TreeNode node)
        {
            //根据Node属性生成XML文本
            treeNode a = new treeNode((treeNode)(node.Tag));
            string rssText = "<Node Name=\"" + node.Name + "\" Text=\"" + node.Text + "\" name=\"" + a.name + "\" time=\"" + a.time + "\" type=\"" + a.type + "\" size=\"" + a.size + "\" filetext=\"" + a.filetext + "\">";
            return rssText;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //写文件头部内容
            //下面是生成RSS的OPML文件
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<Tree>");
            //遍历根节点
            foreach (TreeNode node in Catalogue.Nodes)
            {
                xmlLine = GetRSSText(node);
                sb.Append(xmlLine);
                ParseNode(node, sb);
                sb.Append("</Node>");
            }
            sb.Append("</Tree>");
            StreamWriter sr = new StreamWriter("TreeXml.xml", false, System.Text.Encoding.UTF8);
            sr.Write(sb.ToString());
            sr.Close();
        }
    }
}

    
