using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Units;
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
                    Unit.Random(BattleBehaviour.Panic),
                    Unit.Random(BattleBehaviour.StraightAttack)
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
                Unit.Random(BattleBehaviour.Panic),
                Unit.Random(BattleBehaviour.StraightAttack)
                
            }.Select(x =>
            {
                x.ownerId = userId;
                return x;
            }));
        }

        public void SaveRosters(IEnumerable<Unit> units)
        {
        }

        public void SaveTrainingResult(MatchResultDto result)
        {
        }
    }
}