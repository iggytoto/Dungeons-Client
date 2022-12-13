using System;
using Services.Login;

public class MockLoginService : LoginService
{
    public override ConnectionState ConnectionState => ConnectionState.Connected;

    public override UserContext UserContext => new()
    {
        userId = 1,
        value = "token",
        userName = "Username"
    };


    public override void TryLogin(string login, string password, EventHandler<UserContext> onSuccess)
    {
        onSuccess?.Invoke(this, UserContext);
    }
}