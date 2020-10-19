﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGunController : MonoBehaviour
{
    [SerializeField] LineRenderer freezeLine;
    [SerializeField] AudioSource shootSound;
    [SerializeField] Transform rayOrigin;
    [SerializeField] Material freezeRayMaterial;

    //Firing stats
    [SerializeField] float cooldown = 0.2f;
    private float cooldownTimer = 0f;
    [SerializeField] float shootDistance = 20f;

    //Line stats
    [SerializeField] float lineLifespan = 1f;
    private float lineTimer = 0f;

    private void Update()
    {
        //Update timers
        cooldownTimer += Time.deltaTime;

        //Shoot on left click (and if not paused)
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= cooldown)
        {
            Fire();
        }

        if (freezeLine.enabled)
            FadeLine();
    }

    private void Fire()
    {
        FireFeedback();
        //calculate raycast
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, shootDistance))
        {
            Debug.Log("Hit");
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
        Color c = Color.Lerp(freezeRayMaterial.color, Color.clear, Mathf.Min(lineTimer, 1f));
        freezeLine.material.color = c;
        freezeLine.material.SetColor("_EmissionColor", c);
    }
}