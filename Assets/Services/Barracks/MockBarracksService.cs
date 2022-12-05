using System.Collections.ObjectModel;

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
}