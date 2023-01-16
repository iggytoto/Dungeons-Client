using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common;
using Services.Common.Dto;
using Services.Dto;
using UnityEngine;

namespace Services.TrainingYard
{
    public class TrainingYardService : ServiceBase, ITrainingYardService
    {
        private const string GetRosterForUserPath = "/training/getRosterForUser";
        private const string SaveMatchResultPath = "/training/saveTrainingResult";

        private ILoginService _loginService;

        public new void InitService()
        {
            base.InitService();
            _loginService = FindObjectOfType<GameService>().LoginService;
            Debug.Log(
                $"TrainingYard service adapter configured with endpoint:{APIAdapter.GetConnectionAddress()}");
        }

        public void GetRosterForUser(long userId, EventHandler<IEnumerable<Unit>> onSuccessHandler,
            EventHandler<string> onErrorHandler)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<UnitDto>>(
                    APIAdapter.GetConnectionAddress() + GetRosterForUserPath,
                    ApiAdapter.SerializeDto(new UserIdRequestDto { userId = userId }),
                    ApiAdapter.Get,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    (o, response) => onSuccessHandler.Invoke(o, response.items.Select(dto => dto.ToDomain())),
                    (o, err) => onErrorHandler.Invoke(o, err.message)));
        }

        public async Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId)
        {
            var t = new TaskCompletionSource<IEnumerable<Unit>>();
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ListResponseDto<UnitDto>>(
                    APIAdapter.GetConnectionAddress() + GetRosterForUserPath,
                    ApiAdapter.SerializeDto(new UserIdRequestDto { userId = userId }),
                    ApiAdapter.Get,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    (_, response) => t.SetResult(response.items.Select(dto => dto.ToDomain())),
                    (_, error) => t.SetException(new Exception(error.message))));
            return await t.Task;
        }

        public void SaveTrainingResult(DateTime date, string matchMakingType, long userOneId, long userTwoId,
            long winnerUserId,
            IEnumerable<Unit> processBattleResultsForUnits)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    APIAdapter.GetConnectionAddress() + SaveMatchResultPath,
                    ApiAdapter.SerializeDto(new MatchResultDto
                    {
                        date = date,
                        matchType = matchMakingType,
                        unitsState = processBattleResultsForUnits.Select(UnitDto.Of).ToList(),
                        userOneId = userOneId,
                        userTwoId = userTwoId,
                        winnerUserId = winnerUserId,
                    }),
                    ApiAdapter.Post,
                    APIAdapter.GetAuthHeader(_loginService.UserContext),
                    null,
                    null));
        }
    }
}