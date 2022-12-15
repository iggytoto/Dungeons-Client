using System.Collections.ObjectModel;

namespace Services
{
    public interface ITavernService
    {
        public ObservableCollection<UnitForSale> AvailableUnits { get; }
        public void BuyUnit(Unit unit);
    }
}