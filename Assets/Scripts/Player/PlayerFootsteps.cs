using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstepSound;
    [SerializeField]
    private AudioClip[] footstepClips;
    private CharacterController characterController;
    [HideInInspector]
    public float volumeMin, volumeMax;
    public float accumulatedDistance;
    [HideInInspector]
    public float stepDistance;
    private void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded)
            return;
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstepClips[Random.Range(0, footstepClips.Length)];
                footstepSound.Play();
                accumulatedDistance = 0f;

            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
