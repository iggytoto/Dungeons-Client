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

    public override void InitService()
    {
        _apiAdapter = gameObject.AddComponent<MatchMakingServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        _loginService = FindObjectOfType<GameService>().LoginService;
        Debug.Log(
            $"MM service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
    }

    public void Register(IEnumerable<long> roster, EventHandler<MatchDto> onSuccess,
        EventHandler<ErrorResponseDto> onError)
    {
        _apiAdapter.Register(
            roster,
            _loginService.UserContext,
            (o, r) => onSuccess?.Invoke(o, r.match),
            (o, r) => onError?.Invoke(o, r));
    }

    public void Cancel()
    {
        _apiAdapter.Cancel(_loginService.UserContext, null, null);
    }

    public void Status(EventHandler<MatchDto> onSuccess, EventHandler<ErrorResponseDto> onError)
    {
        if (_loginService?.UserContext == null)
        {
            Debug.LogWarning("Service called without connection context");
            return;
        }

        _apiAdapter.Status(
            _loginService.UserContext,
            (o, r) => onSuccess?.Invoke(o, r.match),
            (o, r) => onError?.Invoke(o, r));
    }

    public void ApplyForServer(string address, string port, EventHandler<MatchDto> onSuccess,
        EventHandler<ErrorResponseDto> onError)
    {
        _apiAdapter.ApplyAsServer(address,
            port,
            _loginService.UserContext,
            (o, r) => onSuccess.Invoke(o, r.match),
            onError);
    }
}