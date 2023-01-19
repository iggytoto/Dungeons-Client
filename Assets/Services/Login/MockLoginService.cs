using System;
using Services;

public class MockLoginService : ILoginService
{
    public ConnectionState ConnectionState => ConnectionState.Connected;

    public UserContext UserContext => new()
    {
        userId = 1,
        value = "token",
        userName = "Username"
    };

    public void TryLogin(string login, string password, Action<UserContext> onSuccess)
    {
        onSuccess?.Invoke(UserContext);
    }

    public void Register(string login, string password)
    {
    }

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}