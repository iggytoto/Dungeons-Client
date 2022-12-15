using System;
using UnityEngine;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;


    private void Update()
    {
        if (Preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            var now = DateTime.Now.Minute;
            now %= 24;
            UpdateLighting(now / 24);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.fogColor.Evaluate(timePercent);
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.directionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation =
                Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        DirectionalLight = RenderSettings.sun != null ? RenderSettings.sun : FindObjectOfType<Light>();
    }
}