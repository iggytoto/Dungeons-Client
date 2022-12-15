using System;
using System.Collections.Generic;
using Services;
using Services.Common;

public class MockMatchMakingService : IMatchMakingService
{
    public void Register(IEnumerable<long> roster)
    {
    }

    public void Cancel()
    {
    }

    public void Status(EventHandler<MatchDto> onSuccess)
    {
        onSuccess?.Invoke(this, new MatchDto
        {
            id = 1,
            serverIpAddress = "127.0.0.1",
            serverPort = "7777",
            status = "ServerFound",
            userOneId = 1,
            userTwoId = 2
        });
    }

    public void ApplyForServer(string address, string port, EventHandler<MatchDto> onSuccess)
    {
        onSuccess?.Invoke(this, new MatchDto
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