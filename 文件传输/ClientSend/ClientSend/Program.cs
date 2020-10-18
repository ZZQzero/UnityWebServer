using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSend
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Sendfile("127.0.0.1",6666, "F:/周期表.rar");
            Console.ReadKey();
        }
    }
}
