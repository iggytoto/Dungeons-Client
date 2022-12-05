using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BarracksService : MonoBehaviour
{
    public virtual ObservableCollection<Unit> AvailableUnits => throw new InvalidOperationException();
}