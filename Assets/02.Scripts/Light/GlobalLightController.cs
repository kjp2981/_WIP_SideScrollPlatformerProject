using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    private Light2D _globalLight;

    [SerializeField, Tooltip("intensity > 1")]
    private float _intensity = 2f;

    [SerializeField, Tooltip("duration > 0")]
    private float _duration = 1f;

    private void Start()
    {
        _globalLight = GetComponent<Light2D>();
    }

    public void FlashGlobalLight(float intensity, float duration)
    {
        StartCoroutine(FlasjGlobalLightCoroutine(intensity, duration));
    }

    public void FlashGlobalLight()
    {
        StartCoroutine(FlasjGlobalLightCoroutine(_intensity, _duration));
    }

    private IEnumerator FlasjGlobalLightCoroutine(float intensity, float duration)
    {
        _globalLight.intensity = intensity;
        float time = duration;
        while (time > 0)
        {
            _globalLight.intensity = Mathf.Lerp(1, intensity, time / duration);
            time -= Time.deltaTime;
            yield return null;
        }
        _globalLight.intensity = 1;
    }
}
