using System.Collections.ObjectModel;
using System.Linq;
using Services;

public class MockBarracksService : IBarrackService
{
    public ObservableCollection<Unit> AvailableUnits { get; } = new()
    {
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
        Unit.Random(),
    };


    public void TrainUnit(long selectedUnitId)
    {
        AvailableUnits.Remove(AvailableUnits.First(u => u.Id == selectedUnitId));
    }
}