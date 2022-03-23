using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Vector2 lookAngles;
    [SerializeField]
    private Transform lookRoot, playerLookRoot;
    private Vector2 currentMouseLook;
    [SerializeField]
    private float sensivity = 5f;
    [SerializeField]
    private bool invert;
    [SerializeField]
    private Vector2 defaultLookLimits = new Vector2(-70f, 80f);
    [SerializeField]
    private bool canUnlock = true;
    [SerializeField]
    private float rollAngle = 10f;
    [SerializeField]
    private float rollSpeed = 3f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockAndUnlockCursor();
        LookAround();
    }

    private void LookAround()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.Y), Input.GetAxis(MouseAxis.X));
            lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
            lookAngles.y += currentMouseLook.y * sensivity;
            lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);
            //rollAngle = Mathf.Lerp(rollAngle, Input.GetAxisRaw(MouseAxis.X) * rollAngle, Time.deltaTime * rollSpeed); lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0, rollAngle); // dizzy effect :)
            lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0, 0); 
            playerLookRoot.localRotation = Quaternion.Euler(0, lookAngles.y, 0); 
        }
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }


}
