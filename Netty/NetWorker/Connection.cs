using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
using System.Collections.Generic;

public class Connection : MonoBehaviour
{
	
	// Update is called once per frame
	void Update () {
        //消息列队
        if (Net.Instance.messageList.Count > 0) {
            messageHandler(Net.Instance.messageList[0]);
            Net.Instance.messageList.RemoveAt(0);
        }
	}
    void OnApplicationQuit()
    {
        Net n = Net.Instance;
        n.close();
    }
    private void messageHandler(Response response) {
        MessageHandlerDAO mhd;
        if (DataPool.Instance.mhdDic.ContainsKey(response.Id))
        { //如果协议对象缓存容器池中有这个协议类
            mhd = DataPool.Instance.mhdDic[response.Id];//根据id从协议对象缓存容器池中获取实例化好的协议类
            mhd = (MessageHandlerDAO)mhd.cloned();//clone出一份
            mhd.MessageDecode(response);//消息分发
            mhd.MessageHandler();//业务处理
        }
        else {
            //如果协议对象缓存容器池中没有这个协议类
            string className = "CMD_"+response.Id;//将服务端发送过来的协议id和CMD_字符串拼接
            Assembly assembly = Assembly.GetExecutingAssembly();//反射
            mhd = (MessageHandlerDAO)assembly.CreateInstance(className);//根据拼接的字符串反射出对相应的协议对象并实例化出来
            DataPool.Instance.mhdDic.Add(response.Id, mhd);//将新类添加到协议对象缓存容器池
            mhd = (MessageHandlerDAO)mhd.cloned();//复制实现类避免多线程数据修改问题。
            mhd.MessageDecode(response);//消息转发
            mhd.MessageHandler();//业务处理
        }
    }
    /// <summary>
    /// 启动该类
    /// </summary>
    public void init() {

    }
}
