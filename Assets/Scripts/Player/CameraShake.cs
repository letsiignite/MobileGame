using UnityEngine;
using System.Collections;
using Game;
using Cinemachine;
using Unity.VisualScripting;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private  CinemachineVirtualCamera playerCamera;
    private void OnEnable()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        while (GameManager._instance == null)
        {
            yield return null;
        }

        GameManager._instance.OnCameraShake += ShakeCamera;
    }


    private void OnDisable()
    {
        GameManager._instance.OnCameraShake -= ShakeCamera;
    }
    private void ShakeCamera()
    {
        StartCoroutine(Shake(2f, 3f));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cinemachineBasicMultiChannelPerlin);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = magnitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = duration;
        yield return new WaitForSeconds(duration);
        while(cinemachineBasicMultiChannelPerlin.m_AmplitudeGain>0)
        {   
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain -= Time.deltaTime;
            if(cinemachineBasicMultiChannelPerlin.m_AmplitudeGain < 0)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
            

    }
}
