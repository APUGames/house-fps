using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 90f;
    [SerializeField] float addIntensity = 1.0f;
    [SerializeField] AudioClip batterySfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(batterySfx, transform.position, 0.5f);

            FlashLightSystem flashlight = other.GetComponentInChildren<FlashLightSystem>();

            if (flashlight != null)
            {
                Debug.Log("Found FlashLightSystem on object: " + flashlight.gameObject.name);
                flashlight.RestoreLightAngle(restoreAngle);
                flashlight.AddLightIntensity(addIntensity);
            }
            else
            {
                Debug.Log("FlashLightSystem not found on object: " + other.gameObject.name);
            }

            Destroy(gameObject);
        }
    }
}
