using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IFilterWordService : IServiceSupport
    {
        long Add(String wordPattern, String replaceWord);
        FilterWordDTO GetById(long id);

        FilterWordDTO[] GetAll();
        FilterWordDTO[] GetAll(int page, int limit);
    }

}
