using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocal;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Connect("127.0.0.1",6666);
            Userinfo userinfo = new Userinfo();

            Console.WriteLine("名字：");
            userinfo.Name= Console.ReadLine();
            Console.WriteLine("密码：");
            userinfo.Pwd = Console.ReadLine();
            Protocaltest protocaltest = new Protocaltest();
            protocaltest.model = 1;
            protocaltest.operat = 1;
            protocaltest.data = userinfo;
            client.SendMessage(protocaltest);
            //while (str!="Quit")
            //{
            //    client.SendMessage(str);
            //    str = Console.ReadLine();

            //}
            Console.ReadKey();
        }
    }
}
