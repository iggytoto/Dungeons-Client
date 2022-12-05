using System.Collections.ObjectModel;

public class MockPlayerBankService : PlayerBankService
{
    private readonly ObservableCollection<Currency> _account = new()
    {
        Gold.Of(100000)
    };

    public override ObservableCollection<Currency> Account => _account;
}