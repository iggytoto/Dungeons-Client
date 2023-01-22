using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Ui.Scenes.Town
{
    public class UnitListPanelUiController : MonoBehaviour
    {
        [SerializeField] private GameObject unitButtonPrefab;
        [SerializeField] private GameObject content;

        public event Action<Unit> OnUnitClicked;

        protected readonly List<UnitButtonUiController> UnitButtonControllers = new();

        public void AddUnit(Unit unit)
        {
            if (UnitButtonControllers.Any(x => x.Unit.Id == unit.Id)) return;
            var button = Instantiate(unitButtonPrefab, content.transform);
            var buttonController = button.GetComponent<UnitButtonUiController>();
            buttonController.OnClick += (_, u) => OnUnitClicked?.Invoke(u);
            buttonController.Unit = unit;
            UnitButtonControllers.Add(buttonController);
        }

        public void RemoveUnit(Unit unit)
        {
            var bc = UnitButtonControllers.FirstOrDefault(x => x.Unit.Id == unit.Id);
            if (bc == null) return;
            Destroy(bc.gameObject);
            UnitButtonControllers.Remove(bc);
        }

        protected void ClearUnits()
        {
            foreach (var unitButtonUiController in UnitButtonControllers.ToList())
            {
                RemoveUnit(unitButtonUiController.Unit);
            }
        }
    }
}