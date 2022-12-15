using System.Collections.ObjectModel;

namespace Services
{
    /**
     * Barracks functions service
     */
    public interface IBarrackService 
    {
        /**
         * Collection of available units for player
         */
        public ObservableCollection<Unit> AvailableUnits { get; }


        /**
         * Sends command to the server to train unit with selected id
         */
        public void TrainUnit(long unitId);
    }
}