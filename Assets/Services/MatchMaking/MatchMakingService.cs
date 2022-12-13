using System.Collections.Generic;
using Services.Common;
using Services.Dto;
using Services.MatchMaking;
using UnityEngine;
using UnityEngine.Events;

public class MatchMakingService : MonoBehaviour
{

    private LoginService _loginService;
    private MatchMakingServiceApiAdapter _apiAdapter;

    public UnityEvent<MatchDto> matchStatusReceived = new();

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<MatchMakingServiceApiAdapter>();
        _loginService = FindObjectOfType<LoginService>();
    }

    public virtual void Register(IEnumerable<long> roster)
    {
        _apiAdapter.Register(roster, _loginService.UserContext, null, OnError);
    }

    public virtual void Cancel()
    {
        _apiAdapter.Cancel(_loginService.UserContext, null, OnError);
    }

    public virtual void Status()
    {
        _apiAdapter.Status(_loginService.UserContext, OnStatusSuccess, OnError);
    }

    public virtual void ApplyForServer(string address, string port)
    {
        _apiAdapter.ApplyAsServer(address, port, _loginService.UserContext, OnApplyForServerSuccess, OnError);
    }

    private void OnStatusSuccess(object sender, MatchMakingStatusResponse e)
    {
        matchStatusReceived.Invoke(e.match);
    }

    private void OnApplyForServerSuccess(object sender, MatchMakingApplyAsServerResponse e)
    {
        matchStatusReceived.Invoke(e.match);
    }

    private void OnError(object sender, ErrorResponse e)
    {
        Debug.Log(e.message);
    }
}