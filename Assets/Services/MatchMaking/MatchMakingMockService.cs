using System.Collections.Generic;
using Services.Common;

public class MatchMakingMockService : MatchMakingService
{
    public override void Register(IEnumerable<long> roster)
    {
    }

    public override void Cancel()
    {
    }

    public override void Status()
    {
        matchStatusReceived?.Invoke(new MatchDto
        {
            id = 1,
            serverIpAddress = "127.0.0.1",
            serverPort = "7777",
            status = "ServerFound",
            userOneId = 1,
            userTwoId = 2
        });
    }

    public override void ApplyForServer(string address, string port)
    {
        matchStatusReceived?.Invoke(new MatchDto
        {
            id = 1,
            serverIpAddress = "127.0.0.1",
            serverPort = "7777",
            status = "ServerFound",
            userOneId = 1,
            userTwoId = 2
        });
    }
}