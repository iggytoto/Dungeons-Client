using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RenameUnitModalController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private UnityEvent<string> okClicked = new();
    [SerializeField] private UnityEvent<string> cancelClicked = new();

    public string GetInputText()
    {
        return nameInput.text;
    }

    public void ShowModal()
    {
        gameObject.SetActive(true);
    }

    public void OnCancelClicked()
    {
        cancelClicked.Invoke(GetInputText());
        gameObject.SetActive(false);
    }

    public void OnOkClicked()
    {
        okClicked.Invoke(GetInputText());
        gameObject.SetActive(false);
    }
}