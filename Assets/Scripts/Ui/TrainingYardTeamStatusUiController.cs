using System.Collections.Generic;
using System.Collections.Specialized;
using Services;
using UnityEngine;

public class TrainingYardTeamStatusUiController : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject unitListPanelPrefab;
    private TrainingBattleFlowController _trainingBattleFlowController;
    private ILoginService _loginService;
    private readonly List<TrainingYardUnitStatusPanelController> _contentData = new();

    private void Start()
    {
        _trainingBattleFlowController = FindObjectOfType<TrainingBattleFlowController>();
        _trainingBattleFlowController.UnitStatuses.CollectionChanged += OnUnitInfoReceived;
        _loginService = FindObjectOfType<GameService>().LoginService;
    }

    private void OnUnitInfoReceived(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Unit newUnit in e.NewItems)
            {
                if (newUnit.ownerId != _loginService.UserContext.userId) continue;
                var go = Instantiate(unitListPanelPrefab, content.transform);
                var c = go.GetComponent<TrainingYardUnitStatusPanelController>();
                c.unit = newUnit;
                _contentData.Add(c);
            }
        }

        if (e.OldItems == null) return;
        foreach (Unit removedUnit in e.OldItems)
        {
            var cd = _contentData.Find(x => x.unit == removedUnit);
            _contentData.Remove(cd);
            Destroy(cd.gameObject);
        }
    }
}