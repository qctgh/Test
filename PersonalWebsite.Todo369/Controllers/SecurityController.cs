using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper.Security;
using PersonalWebsite.Todo369.Models;

namespace PersonalWebsite.Todo369.Controllers
{
    public class SecurityController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            SecurityIndexModel model = new SecurityIndexModel();
            model.Current = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(SecurityIndexModel model, string id, string type)
        {
            model.Current = id;
            switch (id)
            {
                case "aes":
                    if (string.IsNullOrEmpty(model.AESText))
                    {
                        return View(model);
                    }
                    if (type == "encrypt")
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(model.AESKey))
                            {
                                model.AESResult = "请输入秘钥";
                            }
                            else
                            {
                                model.AESResult = AES.EncryptText(model.AESText, model.AESKey);
                            }
                        }
                        catch
                        {
                            model.AESResult = "我太难了，不会加密了";
                        }
                    }
                    if (type == "decrypt")
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(model.AESKey))
                            {
                                model.AESResult = "请输入秘钥";
                            }
                            else
                            {
                                model.AESResult = AES.DecryptText(model.AESText, model.AESKey).Replace("\0", "");
                            }
                        }
                        catch
                        {
                            model.AESResult = "我太难了，解不出来";
                        }
                    }
                    break;
                case "base64":
                    if (string.IsNullOrEmpty(model.Base64Text))
                    {
                        return View(model);
                    }
                    if (type == "encrypt")
                    {
                        try
                        {
                            model.Base64Result = Base64.Base64Encode(model.Base64Text);
                        }
                        catch
                        {
                            model.Base64Result = "我太难了，不会加密了";
                        }
                    }
                    if (type == "decrypt")
                    {
                        try
                        {
                            model.Base64Result = Base64.Base64Decode(model.Base64Text);
                        }
                        catch
                        {
                            model.Base64Result = "我太难了，解不出来";
                        }
                    }
                    break;
                case "des":
                    if (string.IsNullOrEmpty(model.DESText))
                    {
                        return View(model);
                    }
                    if (type == "encrypt")
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(model.DESKey))
                            {
                                model.DESResult = Des.DesEncrypt(model.DESText);
                            }
                            else
                            {
                                model.DESResult = Des.DesEncrypt(model.DESText, model.DESKey);
                            }
                        }
                        catch
                        {
                            model.DESResult = "我太难了，不会加密了";
                        }
                    }
                    if (type == "decrypt")
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(model.DESKey))
                            {
                                model.DESResult = Des.TripDesDecrypt(model.DESText);
                            }
                            else
                            {
                                model.DESResult = Des.TripDesDecrypt(model.DESText, model.DESKey);
                            }
                        }
                        catch
                        {
                            model.DESResult = "我太难了，解不出来";
                        }
                    }
                    break;
                case "md5":
                    if (string.IsNullOrEmpty(model.MD5Text))
                    {
                        return View(model);
                    }
                    if (type == "encrypt")
                    {
                        try
                        {
                            model.MD5Result = MD5Utility.MD5Encrypt(model.MD5Text);
                        }
                        catch
                        {
                            model.MD5Result = "我太难了，不会加密了";
                        }
                    }
                    break;
                case "rsa":

                    break;
                case "url":
                    if (string.IsNullOrEmpty(model.URLText))
                    {
                        return View(model);
                    }
                    if (type == "encrypt")
                    {
                        try
                        {
                            model.URLResult = SafeUrl.SetUrlParam(model.URLText, true);
                        }
                        catch
                        {
                            model.URLResult = "我太难了，不会加密了";
                        }
                    }
                    break;
            }
            return View(model);
        }
    }
}