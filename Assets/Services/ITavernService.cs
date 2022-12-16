using System.Collections.ObjectModel;
using DefaultNamespace;

namespace Services
{
    public interface ITavernService : IUnitListProvider<UnitForSale> ,IService 
    {
        public ObservableCollection<UnitForSale> AvailableUnits { get; }
        public void BuyUnit(Unit unit);

        ObservableCollection<UnitForSale> IUnitListProvider<UnitForSale>.Units => AvailableUnits;
    }
}