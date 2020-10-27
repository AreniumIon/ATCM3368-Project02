using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource jumpSound;

    public float speed = 8f;
    public float sprintMod = 1.8f;
    public float jumpHeight = 3f;

    [SerializeField] Transform groundCheck;
    public float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //vertical clamp
    float minYVelocity = Physics.gravity.y * 2f;
    float maxYVelocity = Physics.gravity.y * -5f;

    private void Update()
    {
        //Ground detection
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;

        //Sprinting
        float finalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            finalSpeed *= sprintMod;
        

        //Apply movement
        controller.Move(direction * finalSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            jumpSound.Play();
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, minYVelocity, maxYVelocity);

        controller.Move(velocity * Time.deltaTime);
    }
}
