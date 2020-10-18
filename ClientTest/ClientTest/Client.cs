using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Protocal;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClientTest
{
    
    public class Client
    {
        private Socket socket;
        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }
        public void Connect(string ip,int port)
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip),port);
            socket.Connect(endPoint);
            Console.WriteLine("连接成功");
            Thread threadReceive = new Thread(Receive);
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }
        public void SendMessage(Protocaltest protocaltest)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, protocaltest);
            byte[] msg = stream.GetBuffer();
            socket.Send(msg);

            //socket.Send(Encoding.UTF8.GetBytes(msg));

        }

        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] msg = new byte[1024];
                    int msglen = socket.Receive(msg);
                    string str = Encoding.UTF8.GetString(msg,0,msglen);
                    Console.WriteLine(str);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("服务器断开");
            }         
        }
    }
}
