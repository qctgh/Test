using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service;
using PersonalWebsite.Service.Entity;

namespace ZSZ.Service
{
    public class CommentService : ICommentService
    {
        public long Add(long articleId, string content, string ip, bool isVisible)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                CommentEntity comment = new CommentEntity();
                comment.ArticleId = articleId;
                comment.Content = content;
                comment.IP = ip;
                comment.IsVisible = isVisible;
                ctx.Comments.Add(comment);
                ctx.SaveChanges();
                return comment.Id;
            }
        }

        public string AddComment(long articleId, string content, string ip)
        {
            string result = "";
            if (content.Contains("<") || content.Contains(">"))
            {
                result = "服务器返回错误";
            }
            using (MyDbContext ctx = new MyDbContext())
            {
                CommentEntity comment = new CommentEntity();
                comment.ArticleId = articleId;
                comment.Content = content;
                comment.IP = ip;
                ctx.Comments.Add(comment);
                ctx.SaveChanges();
            }

            return result;
        }


        public CommentDTO[] GetByArticleId(long articleId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Comments.Where(p => p.ArticleId == articleId).OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();
            }
        }

        private CommentDTO ToDTO(CommentEntity entity)
        {
            CommentDTO dto = new CommentDTO();
            dto.Id = entity.Id;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.ArticleId = entity.ArticleId;
            dto.Content = entity.Content;
            dto.IP = entity.IP;
            dto.IsVisible = entity.IsVisible;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = entity.DeletedDateTime;
            return dto;
        }
    }


}
