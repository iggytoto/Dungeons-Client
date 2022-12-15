using System.Collections.ObjectModel;

namespace DefaultNamespace
{
    public interface IUnitListProvider<TUnit> where TUnit : Unit
    {
        public ObservableCollection<TUnit> Units { get; }
    }
}