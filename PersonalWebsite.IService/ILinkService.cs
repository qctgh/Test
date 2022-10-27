using PersonalWebsite.DTO;
using System;

namespace PersonalWebsite.IService
{
    public interface ILinkService : IServiceSupport
    {
        //类别名，名字
        long Add(string name, string url, string icon, string describe, int orderIndex);

        void Edit(long id, string name, string url, string icon, string describe, int orderIndex);

        LinkDTO[] GetAll();
        LinkDTO[] GetAll(int pageSize, int currentIndex);
        long Count();
    }
}
