using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DataPool {
    private static DataPool _instance;
    //协议缓存容器池
    public Dictionary<int, MessageHandlerDAO> mhdDic = new Dictionary<int, MessageHandlerDAO>();
    public static DataPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DataPool();
            return _instance;
        }

    }
}
