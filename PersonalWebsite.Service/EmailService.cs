using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.Extensions.Logging;
using PersonalWebsite.Helper;
using PersonalWebsite.Helper.Security;
using PersonalWebsite.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace PersonalWebsite.Service
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly MyDbContext ctx;
        private readonly IMailService MailService;
        private readonly IWorkService WorkService;
        public EmailService(ILogger<EmailService> logger, MyDbContext ctx, IMailService MailService, IWorkService WorkService)
        {
            _logger = logger;
            this.ctx = ctx;
            this.MailService = MailService;
            this.WorkService = WorkService;
        }
        /// <summary>
        ///发送通知邮件
        /// </summary>
        /// <param name="userId"></param>
        public void SendNoticeEmail()
        {
            string title = "早教启蒙提醒你来陶冶情操了";
            string content = $"中国古琴十大名曲、六经朗读、中国古典音乐、唐诗三百首、先秦两汉、诗经、欧洲史和中国历史故事，总有你喜欢的。";
            string smtpUserName = ctx.KeyValues.First(p => p.Key == "smtpUserName").Value;
            var now = DateTime.Now;
            string time = now.Hour.ToString();
            string week = DateTimeHelper.WeekToNumber(now.DayOfWeek);
            //获取当前时间的定时配置
            var timings = ctx.Timings.Where(p => p.Time == time && p.Weeks.Contains(week)).ToList();
            //记录开始发送邮件
            WorkService.Add($"/*现在是{now.DayOfWeek}的{time}时，开始发送邮件");
            foreach (var item in timings)
            {
                var user = ctx.Users.First(p => p.Id == item.UserId);
                string result = SendEmail(user.Email, title, content);

                //记录发送邮件情况
                long mailId = MailService.AddValidateMail("1", smtpUserName, user.Email, title, content, result);
                //记录定时作业
                WorkService.Add(user.Id, item.Id, mailId);
            }

            //记录结束发送邮件
            WorkService.Add($"现在是{now.DayOfWeek}的{time}时，结束发送邮件*/");
        }

        /// <summary>
        /// 发送验证邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string SendValidateEmail(long userId, string email, string vcode)
        {
            string validateEmail = ctx.KeyValues.FirstOrDefault(p => p.Key == "ValidateEmail")?.Value;
            if (string.IsNullOrEmpty(validateEmail))
            {
                validateEmail = "https://todo369.top/zaojiao/user/validateuser/";
            }
            string title = "验证你的 早教启蒙 个人电子邮件地址";
            string validateUrl = validateEmail + Des.DesEncrypt($"{userId}&{email}&{vcode}");
            string content = $"你好！<br/>您申请使用此电子邮件地址访问你的 早教启蒙。点击下方链接验证电子邮件地址。 （如果看不到超链接，请把网址粘贴到您的浏览器打开）：<br/><a href='{validateUrl}'>{validateUrl}</a>";
            string result = SendEmail(email, title, content);
            string smtpUserName = ctx.KeyValues.First(p => p.Key == "smtpUserName").Value;
            //记录发送邮件情况
            MailService.AddValidateMail("1", smtpUserName, email, title, content, result);
            return result;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strEmail"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string SendEmail(string strEmail, string title, string content)
        {
            string smtpUserName = ctx.KeyValues.First(p => p.Key == "smtpUserName").Value;
            string smtpPwd = ctx.KeyValues.First(p => p.Key == "smtpPwd").Value;
            string smtpHost = ctx.KeyValues.First(p => p.Key == "SmtpHost").Value;
            //如果使用smtp服务发送邮件必须要设置smtp服务信息
            SmtpClient smtp = new SmtpClient
            {
                //smtp服务器地址(我这里以126邮箱为例，可以依据具体你使用的邮箱设置)
                Host = smtpHost,
                UseDefaultCredentials = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //这里输入你在发送smtp服务器的用户名和密码
                Credentials = new NetworkCredential(smtpUserName, smtpPwd)
            };
            //设置默认发送信息
            Email.DefaultSender = new SmtpSender(smtp);
            var email = Email
                //发送人
                .From(smtpUserName)
                //收件人
                .To(strEmail)
                //抄送人
                .CC(smtpUserName)
                //邮件标题
                .Subject(title)
            //邮件内容
            //.Body(content);
            //如果你发送的内容中包含html格式的内容可以使用如下方式，只需要额外设置第二个参数为true即可
            .Body(content, true);
            //本文以outlook名义发送邮件，不会被当作垃圾邮件  
            //email.Header("X-Priority", "3").Header("X-MSMail-Priority", "Normal").Header("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869").Header("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869").Header("ReturnReceipt", "1");
            SendResponse result = null;
            try
            {
                //依据发送结果判断是否发送成功
                result = email.Send();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

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
        /// <summary>
        /// 发送邮件给多人(适合内部人员使用，会暴露其它人的邮箱)
        /// </summary>
        /// <param name="strEmails"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string SendEmail(List<string> strEmails, string title, string content)
        {
            string smtpUserName = ctx.KeyValues.First(p => p.Key == "smtpUserName").Value;
            string smtpPwd = ctx.KeyValues.First(p => p.Key == "smtpPwd").Value;
            string smtpHost = ctx.KeyValues.First(p => p.Key == "SmtpHost").Value;
            //如果使用smtp服务发送邮件必须要设置smtp服务信息
            SmtpClient smtp = new SmtpClient
            {
                //smtp服务器地址(我这里以126邮箱为例，可以依据具体你使用的邮箱设置)
                Host = smtpHost,
                UseDefaultCredentials = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //这里输入你在发送smtp服务器的用户名和密码
                Credentials = new NetworkCredential(smtpUserName, smtpPwd)
            };
            //设置默认发送信息
            Email.DefaultSender = new SmtpSender(smtp);

            List<Address> toUsers = strEmails.Select(i => new Address { EmailAddress = i }).ToList();
            var email = Email
                //发送人
                .From(smtpUserName)
                //抄送人
                .CC(smtpUserName)
                //多人
                .To(toUsers)
                //邮件标题
                .Subject(title)
            //邮件内容
            //.Body(content);
            //如果你发送的内容中包含html格式的内容可以使用如下方式，只需要额外设置第二个参数为true即可
            .Body(content, true);
            //本文以outlook名义发送邮件，不会被当作垃圾邮件  
            //email.Header("X-Priority", "3").Header("X-MSMail-Priority", "Normal").Header("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869").Header("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869").Header("ReturnReceipt", "1");
            SendResponse result = null;
            try
            {
                //依据发送结果判断是否发送成功
                result = email.Send();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

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
