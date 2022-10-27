# PeronsalWebsite

## 介绍

个人网站项目有4个网站,分别是Todo369、360Blog、早教启蒙和后台管理系统；

 **Todo369网站** 是一个社区网站，用于分享个人文章、经验和技术等，首页有置顶、最近文章、今日推荐和本周最热评论等，在文章详情页面还支持点赞和评论，用户评论时会对内容进行检索，若内容中含有非法词则禁用，含有不太明确的词会在后台进行审核，还支持对内容关键词替换（比如内容中包括“政府”，则给替换成“天朝”）；还有一些小工具，比如HTML格式化、正则表达式、加密解密、人民币转换大写等。本站基于ElasticSearch做了搜索引擎优化，但由于服务器配置低带不动停止使用了，用其它的方法代替；与QQ互联做了集成，支持QQ登录。[详情请戳~](https://todo369.top)

 **369BLog网站** 是我个人博客，这个网站只会显示个人发布的文章大部分都是图文的，大多都是精华的文章，与QQ互联做了集成，支持QQ登录，在线留言等。[详情请戳~](https://todo369.top/blog)

 **早教启蒙网站** 是处于对胎教和早教的灵感，开发的有关教育的平台。采用极简的设计便于用户操作，通过音频的方式来达到启蒙的作用。用户通过QQ登录后绑定激活邮箱可以使用定时服务，选择提醒周期系统会提醒用户。[详情请戳~](https://todo369.top/zaojiao)

 **后台管理系统** 是对前台系统的统一管理平台，其中包括三个模块：网站管理、早教启蒙和系统管理；
    
    网站管理功能包括，文章管理、发表文章、批量静态化、频道管理、过滤词管理（用户在前台系统提交评论后，系统会对评论内容进行筛选，内容若出现禁用词无法提交，审核词则需要后台管理员审核）、批量导入过滤词、广告位管理等功能；

    早教启蒙功能包括，歌单管理、歌曲管理、邮件管理、用户定时管理和定时作业管理等；

    系统管理功能包括，配置管理，把系统所有配置统一设计规划到数据中，更方便分布式集群设计；字典管理，把系统中经常用到的类别数据管理到一起，会减少系统冗余，更适合系统维护扩展；在用户管理方面使用Rbac体系，包括用户管理、角色管理、权限管理；系统更新日志管理；

## 软件架构

 **系统框架** ：

    Asp.Net Core 2.2 + EF Core + SQLServer + LayUI + 开源前端框架

 **系统架构** ：

    Service层（Entity + EntityConfig + Service） + IService层 + DTO层 + Web层 + Helper层 + API层

 **使用的组件** ：


    - 用于QQ登录的 Microsoft.AspNetCore.Authentication.QQ；
     
    - 用于发送邮件的 FluentEmail.Smtp；
     
    - 用于搜索引擎优化的 NEST 和 NEST.JsonNetSerializer；
     
    - 用于记录日志的 NLog.Web.AspNetCore；
     
    - 用于缓存的 StackExchange.Redis 和 Microsoft.Extensions.Caching.Redis；
    
    - 用于EFCore 对接 SQLServer 的 Microsoft.EntityFrameworkCore.SqlServer；
    
    - 用于对象存储的 Qiniu.Shared；
    
    - 用于定时任务的 Quartz；
    
    - 用于绘画验证码图像的 System.Drawing.Common；
     
    - 用于API可视化工具的 Swagger（Swashbuckle.AspNetCore）；
     
    - 用于鉴权授权的 JWT等；








