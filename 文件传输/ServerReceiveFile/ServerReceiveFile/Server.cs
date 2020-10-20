using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;
using Protocal;

namespace ServerReceiveFile
{
    public class Server
    {
        private Socket ServerSocket;
        public void Init()
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 6666);
            ServerSocket.Bind(endPoint);
            ServerSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", ServerSocket.LocalEndPoint.ToString());
            Thread thread = new Thread(ListenClient);
            thread.IsBackground = true;
            thread.Start();

        }

        public void Exit()
        {
            ServerSocket.Close();
            ServerSocket = null;
        }

        public void ListenClient()
        {
            while (true)
            {
                if(ServerSocket!=null)
                {
                    try
                    {
                        Socket Client = ServerSocket.Accept();
                        Thread ReceiveThread = new Thread(RevceiveOrCreate);
                        ReceiveThread.IsBackground = true;
                        ReceiveThread.Start(Client);
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("客户端断开连接");
                        break;
                    }
                }
            }
        }

        public void RevceiveOrCreate(object clientSocke)
        {
            Socket client = clientSocke as Socket;

            IPEndPoint clientPoint = (IPEndPoint)client.RemoteEndPoint;
            //获得文件名
            string SendFileName = Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            //获得包大小
            string bagSize = Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            //获得包总数
            int bagCount;
            int.TryParse(Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client)), out bagCount);
            //获得最后一个包的大小
            string bagLast = Encoding.Unicode.GetString(TransferFiles.ReceiveVarData(client));
            Console.WriteLine("文件名:" + SendFileName + "包大小:" + bagSize + "包总数:" + bagCount + "最后一个包的大小:" + bagLast);
            //string FullPath = Path.Combine(Environment.CurrentDirectory, SendFileName);
            string FullPath = @"/Users/mac/Desktop/aaa.mp4";
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create, FileAccess.Write))
            {
                int SendCount = 0;

                while (true)
                {
                    byte[] data = TransferFiles.ReceiveVarData(client);
                    //byte[] data = TransferFiles.ReceiveData(client,(int)fileStream.Length);

                    if (data.Length == 0)
                    {
                        break;
                    }
                    else
                    {
                        SendCount++;
                        fileStream.Write(data, 0, data.Length);
                    }
                }
                // client.Send(Encoding.Unicode.GetBytes("接收完毕"));

                client.Close();
            }

            Console.WriteLine(SendFileName + "接收完毕");

        }
    }
}
