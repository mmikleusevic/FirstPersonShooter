using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    [SerializeField] private float sensitivity;
    
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -45, 45);
        rotationY += mouseX;
        
        playerTransform.localRotation = Quaternion.Euler(rotationX * sensitivity, rotationY * sensitivity, 0);
    }
}
