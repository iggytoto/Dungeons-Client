using System.Collections.ObjectModel;

namespace Services
{
    /**
     * Player accounts service controls player's access to the amount game resources it have.
     */
    public interface IPlayerAccountService
    {
        /**
         * Player can only observe how much resources it have.
         */
        public ObservableCollection<Currency> Account { get; }
    }
}