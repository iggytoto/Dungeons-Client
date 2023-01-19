using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Units.Humans;

namespace Services.TrainingYard
{
    public class MockTrainingYardService : ITrainingYardService
    {
        public void GetRosterForUser(long userId, Action<IEnumerable<Unit>> onSuccessHandler,
            Action<string> onErrorHandler)
        {
            onSuccessHandler?.Invoke(
                new List<Unit>
                {
                    new HumanWarrior(),
                    new HumanArcher(),
                    new HumanSpearman(),
                    new HumanCleric()
                }.Select(x =>
                {
                    x.ownerId = userId;
                    return x;
                }));
        }

        public Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId)
        {
            return Task.FromResult(new List<Unit>
            {
                new HumanWarrior(),
                new HumanArcher(),
                new HumanSpearman(),
                new HumanCleric()
            }.Select(x =>
            {
                x.ownerId = userId;
                return x;
            }));
        }

        public void SaveTrainingResult(DateTime result, string matchMakingType, long userOneId, long userTwoId,
            long winnerUserId,
            IEnumerable<Unit> processBattleResultsForUnits)
        {
        }
    }
}