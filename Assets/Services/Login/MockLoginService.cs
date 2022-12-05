using UnityEngine.SceneManagement;

public class MockLoginService : LoginService
{
    public override ConnectionState ConnectionState => ConnectionState.Disconnected;

    public override UserContext UserContext => new()
    {
        userId = 1,
        value = "token",
        userName = "Username"
    };


    public override void TryLogin(string login, string password)
    {
        ToTownScene();
    }
}