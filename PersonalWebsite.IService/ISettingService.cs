using PersonalWebsite.DTO;
using System;

namespace PersonalWebsite.IService
{
    public interface ISettingService : IServiceSupport
    {
        //设置配置项name的值为value
        void SetValue(String name, String value);//SetValue("SmtpServer","smtp.qq.com")

        SettingDTO GetById(long id);
        //获取配置项name的值
        String GetValue(String name);//GetValue("SmtpServer")

        void SetIntValue(string name, int value);//SetIntValue("秒数",5);

        int? GetIntValue(string name);

        void SetBoolValue(string name, bool value);

        bool? GetBoolValue(string name);

        SettingDTO[] GetAll();
        void Edit(long id, string name, string value);
    }

}
