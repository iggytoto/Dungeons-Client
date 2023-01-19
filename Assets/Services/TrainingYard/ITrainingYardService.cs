using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common;

/**
 * Training yard service. Provides methods for activities in Training arena. This service should be acessed only by servers.
 */
public interface ITrainingYardService
{
    /**
     * Gets roster for given user. 
     * Returns unit models that were setup in roster by user at the time-point of registration.
     */
    public void GetRosterForUser(long userId, Action<IEnumerable<Unit>> onSuccessHandler,
        Action<string> onErrorHandler);

    public Task<IEnumerable<Unit>> GetRosterForUserAsync(long userId);

    /**
     * Saves training match result to database.
     */
    void SaveTrainingResult(
        DateTime result,
        string matchMakingType,
        long userOneId,
        long userTwoId,
        long winnerUserId,
        IEnumerable<Unit> processBattleResultsForUnits);
}