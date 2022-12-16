using System.Collections;
using System.Collections.Generic;
using Services;
using Services.Common;
using Services.Dto;
using Services.MatchMaking;
using UnityEngine;

public class MatchMakingService : ServiceBase, IMatchMakingService
{
    [SerializeField] private int matchWaitTimeOutSeconds = 30;
    public MatchDto MatchContext => _matchContext;
    private static MatchDto _matchContext;

    private ILoginService _loginService;
    private MatchMakingServiceApiAdapter _apiAdapter;

    private void Start()
    {
        _apiAdapter = gameObject.AddComponent<MatchMakingServiceApiAdapter>();
        _apiAdapter.endpointHttp = EndpointHttp;
        _apiAdapter.endpointAddress = EndpointHost;
        _apiAdapter.endpointPort = EndpointPrt;
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    public void Register(IEnumerable<long> roster)
    {
        _apiAdapter.Register(roster, _loginService.UserContext, null, OnError);
        StartCoroutine(UpdateStatus());
    }

    private IEnumerator UpdateStatus()
    {
        var waitingFor = 0;
        while (waitingFor <= matchWaitTimeOutSeconds)
        {
            _apiAdapter.Status(_loginService.UserContext, (_, r) => _matchContext = r.match, OnError);
            waitingFor++;
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }

    public void Cancel()
    {
        _apiAdapter.Cancel(_loginService.UserContext, null, OnError);
        _matchContext = null;
        StopAllCoroutines();
    }

    public void ApplyForServer(string address, string port)
    {
        _apiAdapter.ApplyAsServer(address, port, _loginService.UserContext,
            (_, r) => _matchContext = r.match, OnError);
        StartCoroutine(UpdateStatus());
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        StopAllCoroutines();
        Debug.Log(e.message);
    }
}