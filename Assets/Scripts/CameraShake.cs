using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Update()
    {
       // Debug.Log("POS CAM" + transform.position);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {

            Debug.Log("SAKE CAM");

            float xOffset = Random.Range(-0.05f, 0.05f);
            float yOffset = Random.Range(-0.05f, 0.05f);

            transform.position = new Vector3(PlayerController.instance.transform.position.x + xOffset, PlayerController.instance.transform.position.y + yOffset, -10f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
