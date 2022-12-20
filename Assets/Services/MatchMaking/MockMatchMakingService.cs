using System;
using System.Collections.Generic;
using Services;
using Services.Common;
using Services.Dto;

public class MockMatchMakingService : IMatchMakingService
{

    public void Register(IEnumerable<long> roster, EventHandler<MatchDto> onSuccess, EventHandler<ErrorResponseDto> onError)
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

    public void Cancel()
    {
    }

    public void Status(EventHandler<MatchDto> onSuccess, EventHandler<ErrorResponseDto> onError)
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

    public void ApplyForServer(string address, string port, EventHandler<MatchDto> onSuccess,
        EventHandler<ErrorResponseDto> onError)
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

    public string EndpointHttpType { get; set; }
    public string EndpointAddress { get; set; }
    public ushort EndpointPort { get; set; }

    public void InitService()
    {
    }
}