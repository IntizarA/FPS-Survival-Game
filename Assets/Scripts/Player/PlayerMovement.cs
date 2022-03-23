using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private CharacterController characterController;
    private float verticalVelocity;
    private Vector3 direction;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {

    }

    void Update()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL));
        direction *= speed * Time.deltaTime;
        direction = transform.TransformDirection(direction);
        ApplyGravity();
        characterController.Move(direction);

    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        Jump();
        direction.y = verticalVelocity * Time.deltaTime;
    }

    private void Jump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }
    }
}
