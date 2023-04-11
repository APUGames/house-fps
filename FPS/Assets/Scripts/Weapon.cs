using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;

    [SerializeField] float range = 100.0f;
    [SerializeField] float damage = 30f;

    // [SerializeField] TextMeshProUGUI ammoText;

    [SerializeField] AudioClip gunSound;
    [SerializeField] float gunVolume = 1.0f;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();

        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        /*int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        if(currentAmmo >= 2)
        {
            ammoText.text = currentAmmo.ToString() + " Shells";
        }
        else if (currentAmmo == 1)
        {
            ammoText.text = currentAmmo.ToString() + " Shell";
        }
        else
        {
            ammoText.text = currentAmmo.ToString() + " Shells";
        }*/
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProcessRaycast();
            PlayMuzzleFlash();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            AudioSource.PlayClipAtPoint(gunSound, transform.position, gunVolume);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, range))
        {
            //print("I hit this: " + hit.transform.name);

            CreateHitImpact(hit);

            // Call a method on EnemyHealth that decreases enemy's health
            /*EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);*/
        }
        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1.0f);
    }
}
