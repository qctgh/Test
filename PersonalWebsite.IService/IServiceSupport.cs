using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.IService
{
    //一个标识接口，所有服务都必须实现这个接口
    //这样可以保证只有真正的服务实现类才会被IOC初始化服务
    public interface IServiceSupport
    {
    }
}
