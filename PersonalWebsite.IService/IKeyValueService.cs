using PersonalWebsite.DTO;
using System;

namespace PersonalWebsite.IService
{
    public interface IKeyValueService : IServiceSupport
    {
        //类别名，名字
        long AddNew(String key, String value);
        KeyValueDTO GetById(long id);

        void Edit(long id, string key, string value);

        //获取类别下的IdName（比如所有的民族）
        KeyValueDTO[] GetAll(String typeName);
        /// <summary>
        /// 获取标签
        /// </summary>
        /// <returns></returns>
        TagDTO[] GetTags();
        KeyValueDTO[] GetAll();
        KeyValueDTO[] GetAll(int pageSize, int currentIndex);
        long Count();
    }
}
