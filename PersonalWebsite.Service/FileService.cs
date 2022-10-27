using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace PersonalWebsite.Service
{
    public class FileService : IFileService
    {
        private readonly MyDbContext ctx;
        private readonly IHostingEnvironment _env;
        public FileService(MyDbContext ctx, IHostingEnvironment _env)
        {
            this.ctx = ctx;
            this._env = _env;
        }

        public Stream DownloadFileContent(string guid)
        {
            throw new System.NotImplementedException();
        }

        public FileDTO GetFileInfo(string guid)
        {
            throw new System.NotImplementedException();
        }

        public string UploadFile(IFormFile file, IFormCollection form, long userId)
        {
            if (string.IsNullOrEmpty(userId.ToString())) { throw new ArgumentNullException("userId", "必须提供上传用户标识"); }
            Stream fileStream = file.OpenReadStream();
            string filename = WebUtility.UrlDecode(file.FileName);
            //发送文件到文件服务器
            string guid = PostFile(fileStream, filename, userId);
            return guid;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string PostFile(Stream fileStream, string fileName, long userId)
        {
            if (null == fileStream) return null;

            var result = SaveFile(fileStream, fileName, userId);
            return result;
        }
        /// <summary>
        /// 本地存储
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private string SaveFile(Stream fileStream, string fileName, long userId)
        {
            string result = "";
            DateTime nowDateTime = DateTime.Now;
            FileEntity model = new FileEntity()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = fileName,
                UserId = userId,
                Type = GetFileExtName(fileName),
                CreateDateTime = nowDateTime
            };
            string savepath = "";
            //按照时间构建路径
            savepath = $"{_env.WebRootPath}/filehub/{nowDateTime.ToString("yyyy/MM/dd")}/";

            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }
            // 将文件保存到指定位置
            string filepath = $"{savepath}/{model.Guid}.{GetFileExtName(fileName)}";
            using (var fs = File.Create(filepath))
            {
                fileStream.CopyTo(fs);
                fs.Flush();
            }
            // 计算hash值
            model.Hash = this.ComputeFileMd5(filepath);
            model.Size = fileStream.Length.ToString();

            ctx.Files.Add(model);
            ctx.SaveChanges();
            //这么处理会有一个巨大的坑，如果以后换域名了，以前文章里的图片地址就都不能用了！！！所以直接返回根文件路径，其它地方用到再进行拼接
            //var setting = ctx.Settings.SingleOrDefault(p => p.Name == "文件服务");
            //if (setting != null)
            //{
            //    result = $"{setting.Value}/filehub/{nowDateTime.ToString("yyyy/MM/dd")}/{model.Guid}.{GetFileExtName(fileName)}";
            //}
            //else
            //{
            //    result = $"{model.Guid}.{GetFileExtName(fileName)}";
            //}
            result = $"/filehub/{nowDateTime.ToString("yyyy/MM/dd")}/{model.Guid}.{GetFileExtName(fileName)}";
            return result;
        }
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileExtName(string fileName)
        {
            var index = fileName.LastIndexOf('.') + 1;
            if (index == -1) return "";
            return fileName.Substring(index);
        }

        /// <summary>
        /// 计算文件md5值
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ComputeFileMd5(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] buffer = md5Provider.ComputeHash(fs);
            string result = BitConverter.ToString(buffer);
            result = result.Replace("-", "");
            md5Provider.Clear();
            fs.Close();
            return result;
        }

    }
}
