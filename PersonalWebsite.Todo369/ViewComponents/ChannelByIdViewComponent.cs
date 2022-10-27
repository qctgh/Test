using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Todo369.ViewComponents
{
    public class ChannelByIdViewComponent : ViewComponent
    {
        private readonly IChannelService ChannelService;

        public ChannelByIdViewComponent(IChannelService ChannelService)
        {
            this.ChannelService = ChannelService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id, long parentId)
        {
            var items = await GetItemsAsync(id, parentId);
            return View(items);
        }

        private Task<ChannelModel> GetItemsAsync(long id, long parentId)
        {
            //频道
            var channels = ChannelService.GetAll();

            //todo:这样设计有个问题，子频道必须在父频道后面，不然会报错
            List<ChannelModel> cmList = new List<ChannelModel>();
            //把所有频道整理成树状结构
            foreach (var item in channels)
            {
                //父亲
                if (item.ParentId == 0)
                {
                    ChannelModel channelModel = new ChannelModel();
                    channelModel.Id = item.Id;
                    channelModel.Code = item.Code;
                    channelModel.Name = item.Name;
                    channelModel.ParentId = item.ParentId;
                    cmList.Add(channelModel);
                }
                //儿子
                else
                {
                    //查找cmList中是否已经存在当前频道的父亲
                    var channel = cmList.FirstOrDefault(p => p.Id == item.ParentId);
                    channel.Channels.Add(new ChannelModel { Id = item.Id, Code = item.Code, Name = item.Name, ParentId = item.ParentId });
                }

            }
            ChannelModel model = new ChannelModel()
            {
                Id = id,
                ParentId = parentId,
                Channels = cmList
            };

            return Task.FromResult(model);
        }
    }
}