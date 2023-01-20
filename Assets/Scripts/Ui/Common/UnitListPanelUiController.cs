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

        private readonly List<UnitButtonUiController> _unitButtonControllers = new();

        public void AddUnit(Unit unit)
        {
            if (_unitButtonControllers.Any(x => x.Unit.Id == unit.Id)) return;
            var button = Instantiate(unitButtonPrefab, content.transform);
            var buttonController = button.GetComponent<UnitButtonUiController>();
            buttonController.OnClick += (_, u) => OnUnitClicked?.Invoke(u);
            buttonController.Unit = unit;
            _unitButtonControllers.Add(buttonController);
        }

        public void RemoveUnit(Unit unit)
        {
            var bc = _unitButtonControllers.FirstOrDefault(x => x.Unit.Id == unit.Id);
            if (bc == null) return;
            Destroy(bc.gameObject);
            _unitButtonControllers.Remove(bc);
        }
    }
}