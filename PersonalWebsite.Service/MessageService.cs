using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class MessageService : IMessageService
    {
        private readonly MyDbContext ctx;
        public MessageService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long Add(int appId, long parentId, string content, long userId, string ip, bool isVisible)
        {
            MessageEntity comment = new MessageEntity();
            comment.AppId = appId;
            comment.ParentId = parentId;
            comment.Content = content;
            comment.UserId = userId;
            comment.IP = ip;
            comment.IsVisible = isVisible;
            ctx.Messages.Add(comment);
            ctx.SaveChanges();
            return comment.Id;

        }
        public bool Del(long id)
        {

            var comment = ctx.Messages.Where(p => p.Id == id).FirstOrDefault();
            comment.IsDeleted = true;
            return ctx.SaveChanges() > 0;

        }
        public bool Check(long id)
        {

            var comment = ctx.Messages.Where(p => p.Id == id).FirstOrDefault();
            comment.IsVisible = true;
            return ctx.SaveChanges() > 0;

        }
        public long GetAll(int appId)
        {
            return ctx.Messages.AsNoTracking().Where(p => p.ParentId == 0 && p.AppId == appId).Count();
        }
        public MessageDTO[] GetAll(int appId, int pageSize, int currentIndex)
        {
            var messages = ctx.Messages.Include(p => p.User).AsNoTracking().Where(p => p.ParentId == 0 && p.AppId == appId).OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
            //遍历父级留言，给父级留言填充子级留言
            foreach (var item in messages)
            {
                item.Messages = GetByParentId(appId, item.Id);
            }
            return messages;

        }

        public MessageDTO[] GetByParentId(int appId, long parentId)
        {

            return ctx.Messages.Include(p => p.User).AsNoTracking().Where(p => p.ParentId == parentId && p.AppId == appId).OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();

        }

        public MessageDTO[] GetByParentId(int appId, long parentId, int pageSize, int currentIndex)
        {

            return ctx.Messages.Include(p => p.User).AsNoTracking().Where(p => p.ParentId == parentId && p.AppId == appId).OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }

        private MessageDTO ToDTO(MessageEntity entity)
        {
            MessageDTO dto = new MessageDTO();
            dto.Id = entity.Id;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.CommentDate = DateTimeHelper.DateTimeToString(entity.CreateDateTime);
            dto.AppId = entity.AppId;
            dto.ParentId = entity.ParentId;
            dto.Content = entity.Content;
            dto.UserId = entity.UserId;
            dto.UserName = entity.User?.Name;
            dto.UserAvatar = entity.User?.Avatar;
            dto.IP = entity.IP;
            dto.IsVisible = entity.IsVisible;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = entity.DeletedDateTime;
            return dto;
        }



    }
}
