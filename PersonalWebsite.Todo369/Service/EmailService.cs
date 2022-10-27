using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;

namespace PersonalWebsite.Todo369.Service
{
    public class EmailService
    {

        public string SendEmail()
        {
            //如果使用smtp服务发送邮件必须要设置smtp服务信息
            SmtpClient smtp = new SmtpClient
            {
                //smtp服务器地址(我这里以126邮箱为例，可以依据具体你使用的邮箱设置)
                Host = "smtp.163.com",
                UseDefaultCredentials = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //这里输入你在发送smtp服务器的用户名和密码
                Credentials = new NetworkCredential("qct154878690@163.com", "changjian520")
            };
            //设置默认发送信息
            Email.DefaultSender = new SmtpSender(smtp);
            var email = Email
                //发送人
                .From("qct154878690@163.com")
                //收件人
                .To("154878690@qq.com")
                //抄送人
                //.CC("admin@126.com")
                //邮件标题
                .Subject("测试邮件标题")
                //邮件内容
                .Body("测试邮件内容");
            //如果你发送的内容中包含html格式的内容可以使用如下方式，只需要额外设置第二个参数为true即可
            //.Body("<h1 align="\"center\"" >.NET大法好 </ h1 >< p > 是的, 这一点毛病都没有 </ p > ",true);
            //依据发送结果判断是否发送成功
            var result = email.Send();
            //或使用异步的方式发送
            //await email.SendAsync();
            if (result.Successful)
            {
                //发送成功逻辑
                return null;
            }
            else
            {
                //发送失败可以通过result.ErrorMessages查看失败原因
                return string.Join(";", result.ErrorMessages);
            }
        }
    }
}
