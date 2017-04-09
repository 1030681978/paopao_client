using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public interface MessageHandlerDAO {

    /**
	 * 消息反序列化
	 */
    void MessageDecode(Response _response);

    /**
	 * 消息处理
	 */
    void MessageHandler();
    /*
     *复制实现类避免多线程数据修改问题。
         */
    Object cloned();
}

