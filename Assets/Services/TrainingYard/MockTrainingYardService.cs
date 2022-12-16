using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common;

namespace Services.TrainingYard
{
    public class MockTrainingYardService : ITrainingYardService
    {
        public void GetRosterForUser(long userId, EventHandler<IEnumerable<Unit>> onSuccessHandler,
            EventHandler<string> onErrorHandler)
        {
            onSuccessHandler?.Invoke(this,
                new List<Unit>
                {
                    Unit.Random(),
                    Unit.Random(),
                    Unit.Random(),
                    Unit.Random(),
                    Unit.Random(),
                    Unit.Random()
                });
        }

        public Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId)
        {
            return Task.FromResult<IEnumerable<Unit>>(new List<Unit>
            {
                Unit.Random(),
                Unit.Random(),
                Unit.Random(),
                Unit.Random(),
                Unit.Random(),
                Unit.Random()
            });
        }

        public void SaveRosters(IEnumerable<Unit> units)
        {
        }

        public void SaveTrainingResult(MatchResultDto result)
        {
        }
    }
}