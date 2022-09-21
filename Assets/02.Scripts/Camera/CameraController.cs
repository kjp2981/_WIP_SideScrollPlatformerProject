using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    private float _amplitude = 1, _instansity = 1;

    [SerializeField, Range(0f, 1f)]
    private float _duration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private CinemachineBasicMultiChannelPerlin noise
    {
        get
        {
            if (_noise == null)
            {
                _noise = VCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            return _noise;
        }
    }

    #region CameraShake
    public void CompletePrevFeedback()
    {
        StopAllCoroutines();
        if (noise != null)
            noise.m_AmplitudeGain = 0;
    }

    public void CreateFeedback()
    {
        noise.m_AmplitudeGain = _amplitude;
        noise.m_FrequencyGain = _instansity;
        StartCoroutine(ShakeCoroutine(_duration));
    }

    public void CreateFeedback(float amplitude, float instansity, float duration)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = instansity;
        StartCoroutine(ShakeCoroutine(duration));
    }

    private IEnumerator ShakeCoroutine(float duration)
    {
        float time = duration;
        while (time > 0)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / duration);
            time -= Time.deltaTime;
            yield return null;
        }
        CompletePrevFeedback();
    }
    #endregion
}
