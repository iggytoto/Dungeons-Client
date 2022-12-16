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
     * Save rosters, basically, save units. Just saves given units to database. This is and save-update method.
     */
    public void SaveRosters(IEnumerable<Unit> units);

    /**
     * Saves training match result to database.
     */
    public void SaveTrainingResult(MatchResultDto result);
}