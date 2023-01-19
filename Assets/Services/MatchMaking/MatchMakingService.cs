using System;
using System.Collections.Generic;
using Services;
using Services.Common;
using Services.Dto;
using Services.MatchMaking;
using UnityEngine;

public class MatchMakingService : ServiceBase, IMatchMakingService
{
    private ILoginService _loginService;
    private MatchMakingServiceApiAdapter _apiAdapter;

    public new void InitService()
    {
        _apiAdapter = gameObject.AddComponent<MatchMakingServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    public void Register(IEnumerable<long> roster, Action<MatchDto> onSuccess,
        Action<ErrorResponseDto> onError)
    {
        _apiAdapter.Register(
            roster,
            _loginService.UserContext,
            onSuccess,
            dto => onError?.Invoke(dto));
    }

    public void Cancel()
    {
        _apiAdapter.Cancel(_loginService.UserContext, null, null);
    }

    public void Status(Action<MatchDto> onSuccess, Action<ErrorResponseDto> onError)
    {
        if (_loginService?.UserContext == null)
        {
            Debug.LogWarning("Service called without connection context");
            return;
        }

        _apiAdapter.Status(
            _loginService.UserContext,
            onSuccess,
            dto => onError?.Invoke(dto));
    }

    public void ApplyForServer(string address, string port, Action<MatchDto> onSuccess,
        Action<ErrorResponseDto> onError)
    {
        _apiAdapter.ApplyAsServer(address,
            port,
            _loginService.UserContext,
            dto => onSuccess.Invoke(dto.match),
            onError);
    }
}