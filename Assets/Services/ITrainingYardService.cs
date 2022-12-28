using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Common;

/**
 * Training yard service. Provides methods for activities in Training arena.
 */
public interface ITrainingYardService
{
    /**
     * Gets roster for given user. 
     * Returns unit models that were setup in roster by user at the time-point of registration.
     */
    public void GetRosterForUser(long userId, EventHandler<IEnumerable<Unit>> onSuccessHandler,
        EventHandler<string> onErrorHandler);

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