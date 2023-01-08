using TMPro;
using UnityEngine;

public class ToolTipUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text tooltipText;
    private RectTransform _transform;
    private static ToolTipUiController _instance;

    private void Awake()
    {
        _transform = gameObject.GetComponent<RectTransform>();
        _instance = this;
        gameObject.SetActive(false);
    }

    public void ShowTooltip(string value)
    {
        gameObject.SetActive(true);
        tooltipText.text = value;
        var mp = Input.mousePosition;
        _transform.sizeDelta = new Vector2(tooltipText.preferredWidth + 10, tooltipText.preferredHeight + 10);
        _transform.position =
            new Vector3(mp.x - tooltipText.preferredWidth / 2, mp.y + tooltipText.preferredHeight / 2, 0);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static ToolTipUiController GetInstance()
    {
        return _instance;
    }
}