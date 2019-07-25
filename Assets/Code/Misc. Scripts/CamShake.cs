using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    public CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    public IEnumerator Shake(float amplitudeGain, float frequencyGain, float duration)
    {
        Noise(amplitudeGain, frequencyGain);
        yield return new WaitForSecondsRealtime(duration);
        CheckNoise();
        yield return new WaitForEndOfFrame();
        Noise(0, 0);
    }

    public void CheckNoise()
    {
        if(noise.m_AmplitudeGain == 0 || noise.m_FrequencyGain == 0)
        {
            Debug.Log("shake fucked up");
        }
    }
}
