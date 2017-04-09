using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

public class Net {
    private static Net _instance;
    private Socket socket;
    private ByteArray ioBuffer = new ByteArray();
    private byte[] readBuffer = new byte[1024];
    public List<Response> messageList = new List<Response>();
  //  private Timer t;
    //private 
    public static Net Instance {
        get {
            if (_instance == null) {
                _instance = new Net();
                _instance.init();
            }
            return _instance;
        }
    }
    public void init() {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 11010);
            socket.BeginReceive(readBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuffer);
            //SendHeartBeat();//心跳开始
        }
        catch (Exception e) {
            Debug.Log("服务器连接失败");
        }
    }
    public void write(Request request) {
        ByteArray arr = new ByteArray();
        arr.WriteInt(-777888);//包头
        arr.WriteInt(request.Id);
        arr.WriteInt(request.getDataLength());
        arr.WriteBytes(request.Data);
        try
        {
            socket.Send(arr.Buffer);
        }
        catch (Exception e) {
            socket.Close();
            Debug.Log("网络错误");
        }
    }
    public void close() {
        socket.Close();
       // t.Stop();
       // t.Close();
    }
    
    bool isRead;
    private void ReceiveCallBack(IAsyncResult ar) {
        try {
            //结束异步读取数据并获取数据长度
            int readCount = socket.EndReceive(ar);
            byte[] bytes = new byte[readCount];
            //将接收缓冲池的内容复制到临时消息存储数组
            Buffer.BlockCopy(readBuffer, 0, bytes, 0, readCount);
            ioBuffer.WriteBytes(bytes);
            if (!isRead)
            {
                isRead = true;
                onData();
            }
        }
        catch(Exception e) {
            Debug.Log("远程服务器主动断开连接");
            socket.Close();
            return;
        }
        socket.BeginReceive(readBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuffer);
    }
    /// <summary>
    /// // 数据包的基本长度：包头+1级协议+2级协议+结果码+数据长度；
    /// 每个协议都是一个int类型的基本数据占4个字节
    /// </summary>
    private int BASE_LENGTH = 4 + 4 + 4 + 4 + 4;
    private void onData() {

        //消息长度小于数据基础长度说明包没完整
        if (ioBuffer.Length < 16) {
            isRead = false;
            return;
        }

        //读取定义的消息长度
        while (true)
        {
            int datazie = ioBuffer.ReadInt();
            if (datazie == -777888)
            {
                break;
            }
        }
        ByteArray ioData = new ByteArray();
        int id = ioBuffer.ReadInt();
        int result_Code = ioBuffer.ReadInt();
        int length = ioBuffer.ReadInt();
        if (ioBuffer.Length < length - 16)
        {
            //还原指针
            ioBuffer.Postion = 0;
        }
        ioData.WriteBytes(ioBuffer.Buffer, 16, length);
        ioBuffer.Postion += length;
        byte[] buf = new byte[length];
        buf = ioData.ReadBytes();

        Response response = new Response() ;
        response.Id = id;
        response.Result_Code = result_Code;//结果码
        response.Data = buf;
        messageList.Add(response);//加入列队
        
        ByteArray bytes = new ByteArray();
        bytes.WriteBytes(ioBuffer.Buffer, ioBuffer.Postion, ioBuffer.Length - ioBuffer.Postion);
        ioBuffer = bytes;
        onData();
    }

    /*
    /// <summary>
    /// 心跳检测机制
    /// </summary>

  
    private void heartbeat(object sender, System.Timers.ElapsedEventArgs e) {
        Request request = new Request();

        request.Id = 0;
        byte[] bytes = new byte[1];
        request.Data = bytes;
        write(request);

    }
    private void SendHeartBeat() {
        t = new Timer(1000);
        t.Elapsed += new System.Timers.ElapsedEventHandler(heartbeat);
        t.AutoReset = true;
        t.Enabled = true;
    }
    */
}
