using Microsoft.AspNetCore.Http;
using PersonalWebsite.DTO;
using System.IO;

namespace PersonalWebsite.IService
{
    public interface IFileService : IServiceSupport
    {
        /// <summary>
        /// 上传文件获得GUID
        /// </summary>
        /// <param name="file"></param>
        /// <param name="form"></param>
        /// <param name="userId"></param>
        /// <returns>返回文件的GUID</returns>
        string UploadFile(IFormFile file, IFormCollection form, long userId);
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        FileDTO GetFileInfo(string guid);

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Stream DownloadFileContent(string guid);
    }
}
