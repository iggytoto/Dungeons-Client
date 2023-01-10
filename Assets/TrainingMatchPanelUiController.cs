using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Services;
using Services.Common;
using Services.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingMatchPanelUiController : MonoBehaviour
{
    [SerializeField] private GameObject rosterSelectedUnitButtonPrefab;
    [SerializeField] private GameObject selectedUnitsContainer;
    [SerializeField] private Button registerMatchMakingButton;
    [SerializeField] private Button cancelMatchMakingButton;
    [SerializeField] private TMP_Text matchMakingStatusText;
    [SerializeField] private Button closePanelButton;

    private readonly List<UnitButtonUiController> _unitButtonUiControllers = new();
    private IMatchMakingService _matchMakingService;

    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        registerMatchMakingButton.onClick.AddListener(OnRegisterMatchMakingClicked);
        cancelMatchMakingButton.onClick.AddListener(OnCancelMatchMakingClicked);
        registerMatchMakingButton.enabled = false;
        cancelMatchMakingButton.enabled = false;
        closePanelButton.onClick.AddListener(OnClosePanelButton);
    }

    private void OnClosePanelButton()
    {
        OnCancelMatchMakingClicked();
        gameObject.SetActive(false);
        foreach (var unitButtonUiController in _unitButtonUiControllers.ToList())
        {
            OnUnitClickedInternal(this, unitButtonUiController.Unit);
        }
    }

    private void OnCancelMatchMakingClicked()
    {
        _matchMakingService.Cancel();
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = _unitButtonUiControllers.Any();
        StopAllCoroutines();
    }

    private void OnRegisterMatchMakingClicked()
    {
        _matchMakingService.Register(_unitButtonUiControllers.Select(ubc => ubc.Unit.Id).ToList(),
            UpdateStatusAndConnectIfServerFound, OnError);
        cancelMatchMakingButton.enabled = true;
        registerMatchMakingButton.enabled = false;
    }

    private void UpdateStatusAndConnectIfServerFound(object sender, MatchDto e)
    {
        matchMakingStatusText.text = e.status;
        if (e.status == "ServerFound")
        {
            SceneManager.LoadScene(SceneConstants.TrainingYardSceneName);
        }

        StartCoroutine(WaitForMatchCoroutine());
    }

    private IEnumerator WaitForMatchCoroutine()
    {
        yield return new WaitForSeconds(1);
        _matchMakingService.Status(UpdateStatusAndConnectIfServerFound, OnError);
    }

    private void OnError(object sender, ErrorResponseDto e)
    {
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = _unitButtonUiControllers.Any();
        matchMakingStatusText.text = e.message;
    }

    public void AddToRoster(Unit u)
    {
        if (!gameObject.activeSelf || _unitButtonUiControllers.Count > 3)
        {
            return;
        }

        AddToContentAsButton(u);
        if (_unitButtonUiControllers.Any())
        {
            registerMatchMakingButton.enabled = true;
        }
    }

    private void AddToContentAsButton(Unit u)
    {
        if (_unitButtonUiControllers.Any(x => x.Unit.Id == u.Id) ||
            _unitButtonUiControllers.Count == 3) return;
        var button = Instantiate(rosterSelectedUnitButtonPrefab, selectedUnitsContainer.transform);
        var buttonController = button.GetComponent<UnitButtonUiController>();
        buttonController.OnClick += OnUnitClickedInternal;
        buttonController.Unit = u;
        _unitButtonUiControllers.Add(buttonController);
    }

    private void OnUnitClickedInternal(object sender, Unit e)
    {
        var bc = _unitButtonUiControllers.FirstOrDefault(x => x.Unit.Id == e.Id);
        if (bc == null) return;
        Destroy(bc.gameObject);
        _unitButtonUiControllers.Remove(bc);
        if (!_unitButtonUiControllers.Any())
        {
            registerMatchMakingButton.enabled = false;
        }
    }
}