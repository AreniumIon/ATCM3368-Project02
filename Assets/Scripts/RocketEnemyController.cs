﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemyController : Freezable
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 3f;
    private float turnBuffer = .05f;
    [SerializeField] float detectionRange = 15f;

    [Header("Projectile")]
    [SerializeField] float projectileReloadTime = 3f;
    [SerializeField] float distanceToFire = 15f;
    [SerializeField] GameObject rocketProjectile = null;
    [SerializeField] AudioSource projectileFireSound = null;
    [SerializeField] Transform projectileSpawnPoint = null;
    [SerializeField] Transform art = null;

    [Header("Other")]
    Rigidbody rb = null;
    Transform projectileParent = null;
    Transform player = null;
    [SerializeField] LayerMask layerMask = new LayerMask();
    bool canFire = true; //cooldown for firing

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        projectileParent = GameObject.FindGameObjectWithTag("ProjectilesParent").transform;
        player = Transform.FindObjectOfType<PlayerHealth>().transform;
    }

    //RESPOND TO PLAYER
    private void FixedUpdate()
    {
        UpdateFreeze();
        //moves if close to player
        if (!isFrozen && IsPlayerNearby())
        {
            MoveToPlayer();
            TurnToPlayer();
            //shooting projectile
            if (canFire && IsPlayerInRange())
                Shoot();
        }
    }

    //Decides if ship should move
    public bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }
    //Decides if ship should shoot
    bool IsPlayerInRange()
    {
        RaycastHit hit;
        Physics.Raycast(projectileSpawnPoint.position, projectileSpawnPoint.forward, out hit, distanceToFire, layerMask);
        return hit.collider.gameObject.GetComponent<PlayerMovement>() != null;
    }



    //MOVEMENT
    void MoveToPlayer()
    {
        //calculate direction
        Vector3 distanceToPlayer = player.position - transform.position;
        distanceToPlayer.y = 0;
        Vector3 moveDirection = distanceToPlayer.normalized;
        //move
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void TurnToPlayer()
    {
        //calculate rotation
        float turnAmountThisFrame = GetDot(turnSpeed, art.right);
        art.Rotate(0, turnAmountThisFrame, 0);
    }

    //used for MoveShip() and TurnShip()
    float GetDot(float variableSpeed, Vector3 baseDirection)
    {
        float dotResult = Vector3.Dot(baseDirection, (player.position - transform.position).normalized);
        //if close to zero, dont rotate
        if (dotResult <= turnBuffer && dotResult >= -1 * turnBuffer)
            variableSpeed = 0;
        //invert result depending on which side its on
        else if (dotResult < 0f)
            variableSpeed *= -1;
        return variableSpeed;
    }

    //DYING
    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

    //FIRE PROJECTILE
    void Shoot()
    {
        //create projectile
        Instantiate(rocketProjectile, projectileSpawnPoint.position, art.rotation, projectileParent);
        //play projectile effects
        PlayShootFeedback();
        //prevents from shooting again until DelayAction marks the ship as reloaded
        canFire = false;
        StartCoroutine(ReadyToFire());
    }

    void PlayShootFeedback()
    {
        //sound
        projectileFireSound.Play();
    }

    private IEnumerator ReadyToFire()
    {
        yield return new WaitForSeconds(projectileReloadTime);
        canFire = true;
    }

    //Freezing
    public override void Freeze()
    {
        isFrozen = true;
        frozenObject.SetActive(true);
    }

    public override void Unfreeze()
    {
        isFrozen = false;
        frozenObject.SetActive(false);
    }
}
