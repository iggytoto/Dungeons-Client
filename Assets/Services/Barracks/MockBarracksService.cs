using System.Collections.ObjectModel;
using System.Linq;

public class MockBarracksService : BarracksService
{
    public override ObservableCollection<Unit> AvailableUnits => _availableUnits;

    private readonly ObservableCollection<Unit> _availableUnits = new()
    {
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
    };


    public override void TrainUnit(long selectedUnitId)
    {
        _availableUnits.Remove(_availableUnits.First(u => u.Id == selectedUnitId));
    }
}