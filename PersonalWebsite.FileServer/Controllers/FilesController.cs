using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.IService;
using System.Collections.Generic;

namespace PersonalWebsite.FileServer.Controllers
{
    [EnableCors("AllowCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFileService FileService { get; set; }
        public FilesController(IFileService FileService)
        {
            this.FileService = FileService;
        }

        // GET: api/Files
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Files/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }



        [DisableRequestSizeLimit]
        [HttpPost]
        public string Post(IFormFile file)
        {
            //保存文件
            var url = FileService.UploadFile(file, Request.Form, 1);
            string result = JsonConvert.SerializeObject(new { status = 0, msg = "上传成功", url = url });
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
