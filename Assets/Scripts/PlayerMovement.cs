using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    public float speed = 12f;
    public float jumpHeight = 3f;

    [SerializeField] Transform groundCheck;
    public float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * z;

        controller.Move(direction * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
