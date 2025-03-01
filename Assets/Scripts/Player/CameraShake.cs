using UnityEngine;
using System.Collections;
using Game;

public class CameraShake : MonoBehaviour
{
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
        StartCoroutine(Shake(50f, 10f));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPosition + new Vector3(x, y, 0);
            Debug.Log(transform.localPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition; 
    }
}
