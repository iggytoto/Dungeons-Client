using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common;
using Services.Common.Dto;
using Services.Dto;

namespace Services.TrainingYard
{
    public class TrainingYardService : ServiceBase, ITrainingYardService
    {
        private const string GetRosterForUserPath = "/training/getRosterForUser";
        private const string SaveMatchResultPath = "/training/saveTrainingResult";

        public void GetRosterForUser(long userId, Action<IEnumerable<Unit>> onSuccessHandler,
            Action<string> onErrorHandler)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine(
                    GetRosterForUserPath,
                    new UserIdRequestDto { userId = userId },
                    ApiAdapter.Get,
                    dto => onSuccessHandler.Invoke(dto.items.Select(d => d.ToDomain())),
                    dto => onErrorHandler.Invoke(dto.message),
                    new DefaultListResponseDtoDeserializer<UnitDto>(new UnitDtoDeserializer())));
        }

        public async Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId)
        {
            var t = new TaskCompletionSource<IEnumerable<Unit>>();
            StartCoroutine(
                APIAdapter.DoRequestCoroutine(
                    GetRosterForUserPath,
                    new UserIdRequestDto { userId = userId },
                    ApiAdapter.Get,
                    dto => t.SetResult(dto.items.Select(d => d.ToDomain())),
                    dto => t.SetException(new Exception(dto.message)),
                    new DefaultListResponseDtoDeserializer<UnitDto>(new UnitDtoDeserializer())));
            return await t.Task;
        }

        public void SaveTrainingResult(DateTime date, string matchMakingType, long userOneId, long userTwoId,
            long winnerUserId,
            IEnumerable<Unit> processBattleResultsForUnits)
        {
            StartCoroutine(
                APIAdapter.DoRequestCoroutine<ResponseBaseDto>(
                    SaveMatchResultPath,
                    new MatchResultDto
                    {
                        date = date,
                        matchType = matchMakingType,
                        unitsState = processBattleResultsForUnits.Select(UnitDto.Of).ToList(),
                        userOneId = userOneId,
                        userTwoId = userTwoId,
                        winnerUserId = winnerUserId,
                    },
                    ApiAdapter.Post,
                    null,
                    null));
        }
    }
}