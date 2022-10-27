using Microsoft.AspNetCore.Http;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using System;

namespace PersonalWebsite.IService
{
    public interface IFilterWordService : IServiceSupport
    {
        long Add(String wordPattern, String replaceWord);
        FilterWordDTO GetById(long id);

        FilterWordDTO[] GetAll();
        FilterWordDTO[] GetAll(int page, int limit);
        /// <summary>
        /// 获取BANNED列表
        /// </summary>
        /// <returns></returns>
        FilterWordDTO[] GetBanned();
        /// <summary>
        /// 获取MOD列表
        /// </summary>
        /// <returns></returns>
        FilterWordDTO[] GetMod();
        /// <summary>
        /// 获取替换词列表
        /// </summary>
        /// <returns></returns>
        FilterWordDTO[] GetReplace();

        FilterResult FilterMsg(string msg, out string replacemsg);
    }

}
