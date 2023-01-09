using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class ToolTipBehaviorUiController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string toolTipText;
    private ToolTipUiController _controller;


    private void Start()
    {
        _controller = ToolTipUiController.GetInstance();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _controller.ShowTooltip(toolTipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _controller.HideTooltip();
    }
}