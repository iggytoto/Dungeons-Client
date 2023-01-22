using System.Collections;
using System.Linq;
using DefaultNamespace.Ui.Scenes.Town;
using Services;
using Services.Common;
using Services.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingMatchPanelUiController : UnitListPanelUiController
{
    [SerializeField] private Button registerMatchMakingButton;
    [SerializeField] private Button cancelMatchMakingButton;
    [SerializeField] private TMP_Text matchMakingStatusText;
    [SerializeField] private Button closePanelButton;

    private IMatchMakingService _matchMakingService;

    private void Start()
    {
        _matchMakingService = FindObjectOfType<GameService>().MatchMakingService;
        registerMatchMakingButton.onClick.AddListener(OnRegisterMatchMakingClicked);
        cancelMatchMakingButton.onClick.AddListener(OnCancelMatchMakingClicked);
        registerMatchMakingButton.enabled = false;
        cancelMatchMakingButton.enabled = false;
        closePanelButton.onClick.AddListener(OnClosePanelButton);
        OnUnitClicked += OnUnitClickedInternal;
    }

    private void OnClosePanelButton()
    {
        OnCancelMatchMakingClicked();
        gameObject.SetActive(false);
        ClearUnits();
    }

    private void OnCancelMatchMakingClicked()
    {
        _matchMakingService.Cancel();
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = UnitButtonControllers.Any();
        StopAllCoroutines();
    }

    private void OnRegisterMatchMakingClicked()
    {
        _matchMakingService.Register(UnitButtonControllers.Select(ubc => ubc.Unit.Id).ToList(),
            UpdateStatusAndConnectIfServerFound, OnError);
        cancelMatchMakingButton.enabled = true;
        registerMatchMakingButton.enabled = false;
    }

    private void UpdateStatusAndConnectIfServerFound(MatchDto e)
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

    private void OnError(ErrorResponseDto e)
    {
        cancelMatchMakingButton.enabled = false;
        registerMatchMakingButton.enabled = UnitButtonControllers.Any();
        matchMakingStatusText.text = e.message;
    }

    public void AddToRoster(Unit u)
    {
        if (!gameObject.activeSelf || UnitButtonControllers.Count > 3)
        {
            return;
        }

        if (UnitButtonControllers.Count == 3) return;
        AddUnit(u);
        if (UnitButtonControllers.Any())
        {
            registerMatchMakingButton.enabled = true;
        }
    }

    private void OnUnitClickedInternal(Unit unit)
    {
        RemoveUnit(unit);
        if (!UnitButtonControllers.Any())
        {
            registerMatchMakingButton.enabled = false;
        }
    }
}