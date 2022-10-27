using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PersonalWebsite.Service
{
    public class FilterWordService : IFilterWordService
    {
        private readonly MyDbContext ctx;
        private readonly IDistributedCache _distributedCache;
        public FilterWordService(MyDbContext ctx, IDistributedCache distributedCache)
        {
            this.ctx = ctx;
            _distributedCache = distributedCache;
        }
        //重用
        private static string bannedExprKey = typeof(FilterWordService) + "BannedExpr";
        private static string modExprKey = typeof(FilterWordService) + "ModExpr";
        private static string replaceKey = typeof(FilterWordService) + "Replace";


        public long Add(string wordPattern, string replaceWord)
        {
            FilterWordEntity filterWord = new FilterWordEntity();
            filterWord.WordPattern = wordPattern;
            filterWord.ReplaceWord = replaceWord;
            ctx.SaveChanges();
            return filterWord.Id;

        }

        public FilterWordDTO[] GetAll()
        {
            return ctx.FilterWords.Select(p => ToDTO(p)).ToArray();

        }
        public FilterWordDTO[] GetAll(int page, int limit)
        {

            var list = ctx.FilterWords.OrderByDescending(p => p.Id).Skip(limit).Take(page);
            return list.Select(p => ToDTO(p)).ToArray();

        }

        public FilterWordDTO GetById(long id)
        {

            return ToDTO(ctx.FilterWords.SingleOrDefault(p => p.Id == id));

        }

        private FilterWordDTO ToDTO(FilterWordEntity entity)
        {
            FilterWordDTO dto = new FilterWordDTO();
            dto.Id = entity.Id;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.WordPattern = entity.WordPattern;
            dto.ReplaceWord = entity.ReplaceWord;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = entity.DeletedDateTime;
            return dto;
        }

        public FilterWordDTO[] GetBanned()
        {

            return ctx.FilterWords.Where(p => p.ReplaceWord == "{BANNED}").Select(p => ToDTO(p)).ToArray();

        }
        public FilterWordDTO[] GetMod()
        {

            return ctx.FilterWords.Where(p => p.ReplaceWord == "{MOD}").Select(p => ToDTO(p)).ToArray();
        }

        public FilterWordDTO[] GetReplace()
        {

            return ctx.FilterWords.Where(p => p.ReplaceWord != "{MOD}" && p.ReplaceWord != "{BANNED}").Select(p => ToDTO(p)).ToArray();

        }


        public FilterResult FilterMsg(string msg, out string replacemsg)
        {
            string bannedExpr = GetBannedExpr();

            //todo：避免每次进行过滤词判断的时候都进行数据库查询。缓存，把bannedExpr缓存起来。


            string modExpr = GetModExpr();


            //先判断禁用词！有禁用词就不让发，无从谈起审核词
            //Regex.Replace(
            foreach (var word in GetReplaceWords())
            {
                msg = msg.Replace(word.WordPattern, word.ReplaceWord);
            }
            replacemsg = msg;

            if (Regex.IsMatch(replacemsg, bannedExpr))
            {
                return FilterResult.Banned;
            }
            else if (Regex.IsMatch(replacemsg, modExpr))
            {
                return FilterResult.Mod;
            }


            return FilterResult.OK;
        }


        /// <summary>
        /// 取得禁用词的正则表达式
        /// 由方法去处理缓存的问题
        /// </summary>
        /// <returns>返回禁用词正则表达式</returns>
        private string GetBannedExpr()
        {
            //因为缓存是整个网站唯一的实例，所以要保证key的唯一性
            //我的习惯就是在Cache的名字前加一个类名，几乎不会重复
            //项目组规定，缓存的名字都是类型全名+缓存项的名字
            string bannedExpr = GetRedis(bannedExprKey);
            if (!string.IsNullOrEmpty(bannedExpr))//如果缓存中存在，则直接返回
            {
                return bannedExpr;
            }

            var banned = (from word in GetBanned()
                          select word.WordPattern).ToArray();
            bannedExpr = string.Join("|", banned);
            //把特殊字段替换成正则表达式的格式
            bannedExpr = bannedExpr.Replace(@".", @"\.").Replace("{2}", ".{0,2}").Replace(@"\", @"\\");
            //如果不存在则去数据库取，然后放入缓存
            SetRedis(bannedExprKey, bannedExpr);
            return bannedExpr;
        }


        private string GetModExpr()
        {
            string modExpr = GetRedis(modExprKey);
            if (!string.IsNullOrEmpty(modExpr))//如果缓存中存在，则直接返回
            {
                return modExpr;
            }
            var mod = (from word in GetMod()
                       select word.WordPattern).ToArray();
            modExpr = string.Join("|", mod);

            modExpr = modExpr.Replace(@".", @"\.").Replace("{2}", ".{0,2}").Replace(@"\", @"\\");
            //如果不存在则去数据库取，然后放入缓存
            SetRedis(modExprKey, modExpr);
            return modExpr;
        }

        private IEnumerable<FilterWordDTO> GetReplaceWords()
        {
            string replaceExpr = GetRedis(replaceKey);

            if (!string.IsNullOrEmpty(replaceExpr))
            {
                return JsonConvert.DeserializeObject<IEnumerable<FilterWordDTO>>(GetRedis(replaceKey));
            }

            //如果不存在则去数据库取，然后放入缓存
            IEnumerable<FilterWordDTO> data = GetReplace();
            SetRedis(replaceKey, JsonConvert.SerializeObject(data));
            return data;
        }


        #region 扩展方法

        /// <summary>
        /// 获取Redis
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetRedis(string str)
        {
            var valueByte = _distributedCache.Get(str);
            if (valueByte == null)
            {
                return null;
            }
            var valueString = Encoding.UTF8.GetString(valueByte);
            return valueString;
        }
        /// <summary>
        /// 设置Redis
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetRedis(string key, string value)
        {
            _distributedCache.Set(key, Encoding.UTF8.GetBytes(value), new DistributedCacheEntryOptions().SetSlidingExpiration(new TimeSpan(1, 0, 0)));
        }

        private void DeleteRedis(string key)
        {
            _distributedCache.Remove(key);
        }



        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected void SetCookies(HttpContext context, string key, string value, int minutes = 30)
        {
            context.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes),
                IsEssential = true
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(HttpContext context, string key)
        {
            context.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(HttpContext context, string key)
        {
            context.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

        #endregion

    }
}
