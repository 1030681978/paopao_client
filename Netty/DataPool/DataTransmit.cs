using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 事件注册进行消息转发
/// </summary>
public class DataTransmit :MonoBehaviour {
    public static DataTransmit Instance;
    public Action<Response> cmd_1;//无返回值的有参委托
    public Func<int,bool> CMD_0Bool;//有返回值有参的委托
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
    private void AAA(Response response) {

    }
}
