using System;

namespace ClientTest
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