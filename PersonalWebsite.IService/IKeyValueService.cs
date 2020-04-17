using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.IService
{
    public interface IKeyValueService : IServiceSupport
    {
        //类别名，名字
        long AddNew(String typeName, String name);
        KeyValueDTO GetById(long id);

        //获取类别下的IdName（比如所有的民族）
        KeyValueDTO[] GetAll(String typeName);
    }
}
