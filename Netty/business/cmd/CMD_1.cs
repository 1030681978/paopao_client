using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

/// <summary>
/// 注：因为协议实现类都需要被clone所以必须把该类标注为[Serializable]。
/// </summary>
[Serializable]
public class CMD_1 : MessageHandlerDAO
{
    Response response;
    public void MessageDecode(Response _response)
    {
        response = _response;
    }

    public void MessageHandler()
    {
        //事件注册到委托上
        DataTransmit.Instance.cmd_1(response);
    }
    /// <summary>
    /// 深度复制：主要避免多个线程同时访问的时候出现数据被更改的问题，
    /// 复制的效果是为每个线程clone出一个协议类避免同时操作一个数据时产生的数据被修改的问题。
    /// </summary>
    /// <returns></returns>
    public object cloned()
    {
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        BinaryFormatter fromatter = new BinaryFormatter();
        fromatter.Serialize(stream, this);
        stream.Position = 0;
        return fromatter.Deserialize(stream);
    }

}
