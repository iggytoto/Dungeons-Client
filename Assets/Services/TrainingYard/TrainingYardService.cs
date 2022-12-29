using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common;
using Services.Common.Dto;
using UnityEngine;

namespace Services.TrainingYard
{
    public class TrainingYardService : ServiceBase, ITrainingYardService
    {
        private TrainingYardServiceApiAdapter _apiAdapter;
        private ILoginService _loginService;

        public override void InitService()
        {
            _apiAdapter = gameObject.AddComponent<TrainingYardServiceApiAdapter>();
            _loginService = FindObjectOfType<GameService>().LoginService;
            _apiAdapter.endpointHttp = EndpointHttp;
            _apiAdapter.endpointAddress = EndpointHost;
            _apiAdapter.endpointPort = EndpointPrt;
            Debug.Log(
                $"TrainingYard service adapter configured with endpoint:{_apiAdapter.GetConnectionAddress()}");
        }

        public void GetRosterForUser(long userId, EventHandler<IEnumerable<Unit>> onSuccessHandler,
            EventHandler<string> onErrorHandler)
        {
            _apiAdapter.GetRosterForUser(new UserIdRequestDto { userId = userId },
                (o, response) => onSuccessHandler.Invoke(o, response.units.Select(dto => dto.ToDomain())),
                (o, err) => onErrorHandler.Invoke(o, err.message),
                _loginService.UserContext);
        }

        public async Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId)
        {
            var t = new TaskCompletionSource<IEnumerable<Unit>>();
            _apiAdapter.GetRosterForUser(
                new UserIdRequestDto { userId =  userId},
                (_, response) => t.SetResult(response.units.Select(dto => dto.ToDomain())),
                (_, error) => t.SetException(new Exception(error.message)),
                _loginService.UserContext);
            return await t.Task;
        }

        public void SaveTrainingResult(DateTime date, string matchMakingType, long userOneId, long userTwoId,
            long winnerUserId,
            IEnumerable<Unit> processBattleResultsForUnits)
        {
            _apiAdapter.SaveMatchResult(
                new MatchResultDto
                {
                    date = date,
                    matchType = matchMakingType,
                    unitsState = processBattleResultsForUnits.Select(UnitDto.Of).ToList(),
                    userOneId = userOneId,
                    userTwoId = userTwoId,
                    winnerUserId = winnerUserId,
                },
                _loginService.UserContext);
        }
    }
}