using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.Todo369.Controllers
{
    public class VerifycodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region 帮助程序
        public ActionResult ValidateCode()
        {
            byte[] data = null;
            //string code = RandCode(5);
            int res;
            string code = RandCode(100, out res);
            TempData["ValidateCode"] = res;
            //画板
            Bitmap imgCode = new Bitmap(130, 36);
            //画笔
            Graphics gp = Graphics.FromImage(imgCode);
            gp.DrawString(code, new Font("宋体", 15), Brushes.White, new PointF(15, 8));
            Random random = new Random();
            //画噪线
            for (int i = 0; i < 5; i++)
            {
                gp.DrawLine(new Pen(RandColor()), random.Next(imgCode.Width), random.Next(imgCode.Height), random.Next(imgCode.Width), random.Next(imgCode.Height));
            }
            //画噪点
            for (int i = 0; i < 50; i++)
            {
                //指定像素的颜色来画噪点
                imgCode.SetPixel(random.Next(imgCode.Width), random.Next(imgCode.Height), RandColor());
            }

            gp.Dispose();
            //创建内存流
            MemoryStream ms = new MemoryStream();
            imgCode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            data = ms.GetBuffer();
            return File(data, "image/jpeg");
        }

        private System.Drawing.Color RandColor()
        {
            Random random = new Random();
            int red = random.Next(10, 240);
            int green = random.Next(10, 240);
            int blue = random.Next(10, 240);
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// 随机生成指定长度的验证码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        private string RandCode(int len)
        {
            //string words = "1234567890" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz";
            //StringBuilder sb = new StringBuilder();
            //Random random = new Random();
            //for (int i = 0; i < len; i++)
            //{
            //    int index = random.Next(0, words.Length);
            //    char c = words[index];
            //    if (sb.ToString().Contains(c))
            //    {
            //        i--;
            //        continue;
            //    }
            //    sb.Append(words[index] + "");
            //}
            //return sb.ToString();
            string str = Guid.NewGuid().ToString();
            return str.Substring(str.Length - len);
        }
        /// <summary>
        /// 随机生成加法运算的验证码
        /// </summary>
        /// <param name="max"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private string RandCode(int max, out int result)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            int i = random.Next(max), j = random.Next(max);
            sb.Append("= ");
            sb.Append(i + " + " + j);
            result = i + j;
            return sb.ToString();
        }
        #endregion
    }
}