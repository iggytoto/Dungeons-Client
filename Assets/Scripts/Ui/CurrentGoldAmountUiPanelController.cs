using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Services;
using TMPro;
using UnityEngine;

public class CurrentGoldAmountUiPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text goldAmountText;
    [SerializeField] private float updateInterval = 1;
    private float _updateTimer;
    private IPlayerAccountService _playerAccountService;

    private void Start()
    {
        _playerAccountService = FindObjectOfType<GameService>().PlayerAccountService;
    }

    private void Update()
    {
        _updateTimer -= Time.deltaTime;
        if (!(_updateTimer <= 0) || _playerAccountService == null) return;
        goldAmountText.text = _playerAccountService.Account.FirstOrDefault(c => c.Id == Gold.ID)?.Amount
            .ToString(CultureInfo.InvariantCulture);
        _updateTimer = updateInterval;
    }
}