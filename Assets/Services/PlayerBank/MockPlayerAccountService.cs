using System.Collections.ObjectModel;
using Services;

public class MockPlayerAccountService : IPlayerAccountService
{
    public ObservableCollection<Currency> Account { get; } = new()
    {
        Gold.Of(100000)
    };
}