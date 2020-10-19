using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocal
{
    [Serializable]
    public class Userinfo
    {
        private string name;
        private string pwd;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
    }
}
