using System;
using System.Collections.Generic;
using Services;
using Services.Common;

public class MockMatchMakingService : IMatchMakingService
{
    public MatchDto MatchContext { get; private set; }

    public void Register(IEnumerable<long> roster)
    {
    }

    public void Cancel()
    {
    }

    public void ApplyForServer(string address, string port)
    {
        MatchContext = new MatchDto
        {
            id = 1,
            serverIpAddress = "127.0.0.1",
            serverPort = "7777",
            status = "ServerFound",
            userOneId = 1,
            userTwoId = 2
        };
    }
}