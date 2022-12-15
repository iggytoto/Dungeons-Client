using System;
using System.Collections.Generic;
using Services;
using Services.Common;
using Services.Dto;
using Services.MatchMaking;
using UnityEngine;

public class MatchMakingService : MonoBehaviour, IMatchMakingService
{
    private ILoginService _loginService;
    private MatchMakingServiceApiAdapter _apiAdapter;

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<MatchMakingServiceApiAdapter>();
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    public void Register(IEnumerable<long> roster)
    {
        _apiAdapter.Register(roster, _loginService.UserContext, null, OnError);
    }

    public void Cancel()
    {
        _apiAdapter.Cancel(_loginService.UserContext, null, OnError);
    }

    public void Status(EventHandler<MatchDto> onSuccess)
    {
        _apiAdapter.Status(_loginService.UserContext, (source, data) => onSuccess.Invoke(source, data.match), OnError);
    }

    public void ApplyForServer(string address, string port, EventHandler<MatchDto> onSuccess)
    {
        _apiAdapter.ApplyAsServer(address, port, _loginService.UserContext,
            (source, data) => onSuccess.Invoke(source, data.match), OnError);
    }

    private void OnError(object sender, ErrorResponse e)
    {
        Debug.Log(e.message);
    }
}