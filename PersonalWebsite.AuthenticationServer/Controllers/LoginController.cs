using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebsite.IService;
using PersonalWebsite.Service;

namespace PersonalWebsite.AuthenticationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAdminUserService AdminUserService { get; }
        private IJWTService JWTService { get; }

        public LoginController(IAdminUserService AdminUserService, IJWTService JWTService)
        {
            this.AdminUserService = AdminUserService;
            this.JWTService = JWTService;
        }

        // GET: api/Authentication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authentication/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authentication
        [HttpPost]
        public string Post(string name, string password)
        {
            bool isResult = AdminUserService.CheckLogin(name, password);
            if (isResult)
            {
                return JWTService.GetToken(name);
            }
            else
            {
                return null;
            }
        }

        // PUT: api/Authentication/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
