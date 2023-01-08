using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyUnitButtonUiController : MonoBehaviour
{
    [SerializeField] private GameObject buyUnitPopup;

    public void OnClick()
    {
        buyUnitPopup.SetActive(true);
        var mp = Input.mousePosition;
        var popupTransform = buyUnitPopup.GetComponent<RectTransform>().rect;
        buyUnitPopup.transform.position =
            new Vector3(mp.x - popupTransform.width / 2, mp.y + popupTransform.height / 2, 0);
    }
}