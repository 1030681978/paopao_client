using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
  public class Response {
    private int id;
    /// <summary>
    /// 结果码
    /// </summary>
    private int result_Code;
    /// <summary>
    /// 数据
    /// </summary>
    private byte[] data;

   
    public int Result_Code
    {
        get
        {
            return result_Code;
        }

        set
        {
            result_Code = value;
        }
    }

    public byte[] Data
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public int getDataLength() {
        if (data != null)
        {
            return data.Length;
        }
        return 0;
    }
}
