using System;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerBankService : MonoBehaviour
{
    public virtual ObservableCollection<Currency> Account => throw new InvalidOperationException();
}