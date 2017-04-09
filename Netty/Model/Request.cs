using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
 public class Request{
    /// <summary>
    /// 数据id
    /// </summary>
    private int id;
    /// <summary>
    /// 数据
    /// </summary>
    private byte[] data;
    

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

    public int getDataLength()
    {
        if (data != null)
        {
            return data.Length;
        }
        return 0;
    }
}
