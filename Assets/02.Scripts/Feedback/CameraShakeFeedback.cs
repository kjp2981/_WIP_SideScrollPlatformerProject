using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using static Define;

public class CameraShakeFeedback : Feedback
{
    [SerializeField, Range(0, 5)]
    private float _amplitude = 1, _instansity = 1;

    [SerializeField, Range(0f, 1f)]
    private float _duration = 0.1f;

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin _noise;

    private CinemachineBasicMultiChannelPerlin noise
    {
        get
        {
            if(vCam == null)
                vCam = Define.VCam;
            if(_noise == null)
            {
                _noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            return _noise;
        }
    }

    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        if(noise != null)
            noise.m_AmplitudeGain = 0;
    }

    public override void CreateFeedback()
    {
        noise.m_AmplitudeGain = _amplitude;
        noise.m_FrequencyGain = _instansity;
        ShakeCoroutine();
    }

    private IEnumerator ShakeCoroutine()
    {
        //float time = _duration;
        //while(time > 0)
        //{
        //    noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration);
        //    time -= Time.deltaTime;
        //    yield return null;
        //}
        //CompletePrevFeedback();

        noise.m_AmplitudeGain = _amplitude;
        yield return new WaitForSeconds(0.3f);
        noise.m_AmplitudeGain = 0;
    }
}
