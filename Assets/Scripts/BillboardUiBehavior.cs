using UnityEngine;

public class BillboardUiBehavior : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }
}