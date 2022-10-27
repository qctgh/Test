namespace PersonalWebsite.IService
{
    public interface IJWTService : IServiceSupport
    {
        string GetToken(string UserName);
    }
}
