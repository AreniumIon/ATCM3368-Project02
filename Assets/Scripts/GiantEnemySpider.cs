using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemySpider : Freezable
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 3f;
    private float turnBuffer = .05f;
    [SerializeField] float detectionRange = 15f;
    [SerializeField] Transform art = null;

    [Header("Other")]
    Rigidbody rb = null;
    Transform player = null;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }

    //Decides if ship should move
    public bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    //Collide with player
    private void OnTriggerEnter(Collider other)
    {
        if (!isFrozen)
        {
            //if collides with player
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
                audioSource.Play();
            }
        }
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
