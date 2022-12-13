using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerDebugHelperController : MonoBehaviour
{
    [SerializeField] public Button serverButton;
    [SerializeField] public Button hostButton;
    [SerializeField] public Button clientButton;

    private void Start()
    {
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}