using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGunController : MonoBehaviour
{
    [SerializeField] LineRenderer freezeLine;
    [SerializeField] AudioSource shootSound;
    [SerializeField] Transform rayOrigin;
    [SerializeField] Transform playerCamera;
    [SerializeField] Material freezeRayMaterial;
    [SerializeField] Material altFireMaterial;

    //Firing stats
    [SerializeField] float cooldown = 0.2f;
    private float cooldownTimer = 0f;
    [SerializeField] float shootDistance = 20f;

    //Line stats
    [SerializeField] float lineLifespan = 1f;
    private float lineTimer = 0f;
    private bool isLeftClick = true;

    private void Update()
    {
        //Update timers
        cooldownTimer += Time.deltaTime;

        //Shoot on left click (and if not paused)
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= cooldown)
        {
            Fire();
        }
        else if (Input.GetMouseButtonDown(1) && cooldownTimer >= cooldown)
        {
            AltFire();
        }

        if (freezeLine.enabled)
            FadeLine();
    }

    private void Fire()
    {
        FireFeedback();
        //calculate raycast
        RaycastHit objectHit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out objectHit, shootDistance))
        {
            FrozenObject frozenObject = objectHit.collider.gameObject.GetComponent<FrozenObject>();
            if (frozenObject != null)
                frozenObject.RefreshFreeze();
            else
            {
                Freezable freezableHit = objectHit.collider.gameObject.GetComponent<Freezable>();
                if (freezableHit != null)
                    freezableHit.Freeze();
            }

        }
        //reset timer
        cooldownTimer = 0f;
    }

    private void FireFeedback()
    {
        //Audio
        shootSound.Play();
        //Visual
        freezeLine.SetPosition(0, rayOrigin.position);
        freezeLine.SetPosition(1, rayOrigin.position + rayOrigin.forward * shootDistance);
        freezeLine.enabled = true;
        lineTimer = 0f;
        StartCoroutine(DisableFreezeLine());
        isLeftClick = true;
    }

    private void AltFire()
    {
        AltFireFeedback();
        //calculate raycast
        RaycastHit objectHit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out objectHit, shootDistance))
        {
            FrozenObject frozenObject = objectHit.collider.gameObject.GetComponent<FrozenObject>();
            if (frozenObject != null)
                frozenObject.Unfreeze();
        }
        //reset timer
        cooldownTimer = 0f;
    }

    private void AltFireFeedback()
    {
        //Audio
        shootSound.Play();
        //Visual
        freezeLine.SetPosition(0, rayOrigin.position);
        freezeLine.SetPosition(1, rayOrigin.position + rayOrigin.forward * shootDistance);
        freezeLine.enabled = true;
        lineTimer = 0f;
        StartCoroutine(DisableFreezeLine());
        isLeftClick = false;
    }

    private IEnumerator DisableFreezeLine()
    {
        yield return new WaitForSeconds(lineLifespan);
        freezeLine.enabled = false;
    }

    private void FadeLine()
    {
        //Ray fades out over time
        lineTimer += Time.deltaTime / lineLifespan;
        Color c;
        if (isLeftClick)
            c = Color.Lerp(freezeRayMaterial.color, Color.clear, Mathf.Min(lineTimer, 1f));
        else
            c = Color.Lerp(altFireMaterial.color, Color.clear, Mathf.Min(lineTimer, 1f));
        freezeLine.material.color = c;
        freezeLine.material.SetColor("_EmissionColor", c);
    }
}
