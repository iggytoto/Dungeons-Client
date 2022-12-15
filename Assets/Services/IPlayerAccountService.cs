using System.Collections.ObjectModel;

namespace Services
{
    public interface IPlayerAccountService
    {
        public ObservableCollection<Currency> Account { get; }
    }
}