using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Protocal
{
    public class TransferFiles
    {
        public static int SendData(Socket socket, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;

            while (total < size)
            {
                sent = socket.Send(data, total, dataleft, SocketFlags.None);
                total += sent;
                dataleft -= sent;
            }
            return total;
        }

        public static byte[] ReceiveData(Socket socket, int size)
        {
            int total = 0;
            int dataleft = size;
            byte[] data = new byte[size];
            int recv;
            while (total < size)
            {
                recv = socket.Receive(data, total, dataleft, SocketFlags.None);
                if (recv == 0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }

        public static int SendVarData(Socket socket,byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataleft = size;
            int sent;
            byte[] datasize = new byte[4];

            try
            {
                datasize = BitConverter.GetBytes(size);
                sent = socket.Send(datasize);
                while (total < size)
                {
                    sent = socket.Send(data, total, dataleft, SocketFlags.None);
                    total += sent;
                    dataleft -= sent;
                }
                return total;
            }
            catch (Exception)
            {
                Console.WriteLine("发送错误");
                return 3;
            }
        }

        public static byte[] ReceiveVarData(Socket socket)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            recv = socket.Receive(datasize, 0, 4, SocketFlags.None);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total<size)
            {
                recv = socket.Receive(data, total, dataleft, SocketFlags.None);
                if(recv ==0)
                {
                    data = null;
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
    }
}
