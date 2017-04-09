using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DataTransmit.Instance.cmd_1 += demo;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("x")) {
            send();
        }
	}
    private void demo(Response response) {
        print(response.Id);
        print(response.Result_Code);
        ByteArray arr = new ByteArray();
        arr.WriteBytes(response.Data);
        int i = arr.ReadInt();
        print(i);
    }
    private void send() {
        Request request = new Request();
        ByteArray arr = new ByteArray();
        request.Id = 1;
        arr.WriteString("hello paopao");
        byte[] buf = arr.Buffer;
        request.Data = buf;
        Net.Instance.write(request);
    }
}
