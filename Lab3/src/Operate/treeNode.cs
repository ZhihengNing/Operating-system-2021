using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filesystem
{
     class treeNode
    {
        public string name;
        public string time;
        public string type;
        public string size;
        public string filetext;
        public treeNode(string _name,string _time,string _type, string _size,string _filetext)
        {
            name = _name;
            time = _time;
            type = _type;
            size = _size;
            filetext = _filetext;
        }
        public treeNode(treeNode temp) 
        {
            name = temp.name;
            time = temp.time;
            type = temp.type;
            size = temp.size;
            filetext = temp.filetext;
        }

    }
}
