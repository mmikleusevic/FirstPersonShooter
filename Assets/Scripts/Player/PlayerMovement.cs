using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody playerRigidbody;
    
    private Vector3 direction;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.right; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += -transform.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += -transform.forward;
        }
        
        Move();
    }
    
    private void Move()
    {
        Vector3 normalizedDirection = direction.normalized;
        
        playerRigidbody.linearVelocity = 
            new Vector3(normalizedDirection.x * speed, playerRigidbody.linearVelocity.y, normalizedDirection.z * speed);
    }
}