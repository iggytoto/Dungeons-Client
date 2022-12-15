using System;
using System.Collections.Generic;
using Services;
using Services.Common;

public class MockMatchMakingService : IMatchMakingService
{
    public MatchDto MatchContext { get; private set; } = new()
    {
        id = 1,
        serverIpAddress = "127.0.0.1",
        serverPort = "7777",
        status = "ServerFound",
        userOneId = 1,
        userTwoId = 2
    };

    public void Register(IEnumerable<long> roster)
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

    public void Cancel()
    {
        MatchContext = null;
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