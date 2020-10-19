using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Protocal;

public class ClientSocket : MonoBehaviour
{
    // Start is called before the first frame update
    public string path;
    void Start()
    {
        SendFile("127.0.0.1",6666, path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SendFile(string ip,int port,string filePath)
    {
        FileInfo fileInfo=new FileInfo(filePath);
        FileStream fileStream = fileInfo.OpenRead();
        
        int PacketSize = 10000;

        int packetcount = (int)(fileStream.Length / PacketSize);

        int lastDataPacket = (int)(fileStream.Length - (PacketSize * packetcount));

        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket clientSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        
        try
        {
            clientSocket.Connect(endPoint);
        }
        catch (Exception e)
        {
            Debug.LogError("连接服务器失败:"+e);
            return false;
        }
        //发送文件名到客户端
        TransferFiles.SendVarData(clientSocket, Encoding.Unicode.GetBytes(fileStream.Name));
        //发送包大小到服务器
        TransferFiles.SendVarData(clientSocket, Encoding.Unicode.GetBytes(PacketSize.ToString()));
        //发送包数量
        TransferFiles.SendVarData(clientSocket, Encoding.Unicode.GetBytes(packetcount.ToString()));
        //发送[最后一个包的大小]到客户端
        TransferFiles.SendVarData(clientSocket, Encoding.Unicode.GetBytes(lastDataPacket.ToString()));
        bool isCut = false;
        byte[] data=new byte[PacketSize];
        for (int i = 0; i < packetcount; i++)
        {
            fileStream.Read(data, 0, data.Length);
            if (TransferFiles.SendVarData(clientSocket, data) == 3)
            {
                isCut = true;
                return false;
            }
        }
        if (lastDataPacket != 0)
        {
            data=new byte[lastDataPacket];
            fileStream.Read(data, 0, data.Length);
            TransferFiles.SendVarData(clientSocket, data);
        }
        clientSocket.Close();
        fileStream.Close();
        if (!isCut)
        {
            return true;
        }
        return false;
    }
}
