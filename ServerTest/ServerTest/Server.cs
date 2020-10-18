using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Protocal;
using MySql.Data.MySqlClient;

namespace ServerTest
{
    public class Server
    {
        private int Port = 6666;
        private Socket server;
        private List<Socket> AllClient;
        public Server()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            AllClient = new List<Socket>();
        }
        public void StartServer()
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
            server.Bind(endPoint);
            server.Listen(10);
            Console.WriteLine("服务器启动");
            Thread threadAccept = new Thread(Accept);
            threadAccept.IsBackground = true;
            threadAccept.Start();
        }

        private void Accept()
        {
            while (true)
            {
                Socket client = server.Accept();
                AllClient.Add(client);
                IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine(point.Address + "[" + point.Port + "]连接");
                Thread threadRecevie = new Thread(Recevie);
                threadRecevie.IsBackground = true;
                threadRecevie.Start(client);
            }
        }

        private void Recevie(object obj)
        {
           
            Socket client = obj as Socket;

            IPEndPoint point = client.RemoteEndPoint as IPEndPoint;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024*1024];
                    int msglen = client.Receive(buffer);

                    MemoryStream memoryStream = new MemoryStream(buffer, 0, msglen);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    Protocaltest protocaltest = binaryFormatter.Deserialize(memoryStream) as Protocaltest;

                    switch (protocaltest.model)
                    {
                        case 1://用户
                            switch (protocaltest.operat)
                            {
                                case 1://注册
                                    Userinfo userinfo = protocaltest.data as Userinfo;
                                    string sql = "insert into user(name,password) values(@name,@pwd)";
                                    int result = MysqlHelper.Insert(sql, new MySqlParameter("@name", userinfo.Name),
                                        new MySqlParameter("@pwd", userinfo.Pwd));
                                    if(result==1)
                                    {
                                        client.Send(Encoding.UTF8.GetBytes("成功"));
                                        //BroadCast("广播", client);
                                    }
                                    break;
                                case 2://登陆
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 2:
                            break;
                        default:
                            break;
                    }

                    //string msg = Encoding.UTF8.GetString(buffer, 0, msglen);
                    //string str = point.Address + "[" + point.Port + "]:" + msg;
                    //Console.WriteLine(str);

                    //client.Send(Encoding.UTF8.GetBytes(msg + "OJBK"));
                    //BroadCast("广播", client);
                }                
            }
            catch (Exception)
            {
                Console.WriteLine(point.Address + "[" + point.Port + "]:" + "已断开连接");
                AllClient.Remove(client);
            }                                
        }

        private void BroadCast(string msg,Socket ownSocket)
        {
            foreach (var client in AllClient)
            {
                if (client != ownSocket)
                {
                    byte[] buffer = new byte[1024];
                    buffer = Encoding.UTF8.GetBytes(msg);
                    client.Send(buffer);
                }
            }
        }
    }
}
