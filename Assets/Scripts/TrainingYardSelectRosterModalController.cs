using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingYardSelectRosterModalController : MonoBehaviour
{
    [SerializeField] public GameObject modal;


    public void Show()
    {
        modal.SetActive(true);
    }

    public void Hide()
    {
        modal.SetActive(false);
    }

    public void StartTraining()
    {
        modal.SetActive(false);
    }
}