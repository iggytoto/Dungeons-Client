using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class TopPlayerBanAccountPanelController : MonoBehaviour
{
    public TMP_Text goldAmountText;
    private PlayerBankService _playerBankService;
    private float _updateInterval = 1;

    private void Start()
    {
        _playerBankService = FindObjectOfType<PlayerBankService>();
        UpdateAccount();
    }


    private void Update()
    {
        _updateInterval -= Time.deltaTime;
        if (!(_updateInterval <= 0)) return;
        UpdateAccount();
        _updateInterval = 1;
    }

    private void UpdateAccount()
    {
        goldAmountText.text = _playerBankService.Account.First(u => u.Id == Gold.ID).Amount
            .ToString(CultureInfo.InvariantCulture);
    }
}