using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Transform lookRoot;
    private bool isCrouching;
    private float moveSpeed;
    private float crouchHeight = 1f;
    private float normalHeight = 1.6f;
    [SerializeField]
    private float sprintSpeed = 8f;
    [SerializeField]
    private float crouchSpeed = 1.5f;
    private PlayerFootsteps playerFootsteps;
    private float sprintVolume=1f;
    private float sprintStepDistance=0.25f;
    private float crouchStepDistance=0.5f;
    private float crouchVolume=0.1f;
    private float walkVolumeMin=0.2f,walkVolumeMax=0.6f;
    private float walkStepDistance = 0.4f;

    void Awake()
    {
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
        playerMovement = GetComponent<PlayerMovement>();
        moveSpeed = playerMovement.speed;
        lookRoot = transform.GetChild(0);
    }
    private void Start()
    {
        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
        playerFootsteps.stepDistance = walkStepDistance;
    }
    void Update()
    {
        Sprint();
        Crouch();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0, normalHeight, 0);
                playerMovement.speed = moveSpeed;
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;
                playerFootsteps.stepDistance = walkStepDistance;
                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0, crouchHeight, 0);
                playerMovement.speed = crouchSpeed;
                playerFootsteps.volumeMin = crouchVolume;
                playerFootsteps.volumeMax = crouchVolume;
                playerFootsteps.stepDistance = crouchStepDistance;
                isCrouching = true;
            }
        }
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = sprintSpeed;
            playerFootsteps.stepDistance = sprintStepDistance;
            playerFootsteps.volumeMin = sprintVolume;
            playerFootsteps.volumeMax = sprintVolume;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
            playerFootsteps.stepDistance = walkStepDistance;
        }
    }
}
