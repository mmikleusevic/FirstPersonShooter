using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 jumpForce;
    [SerializeField] private List<Gun> guns;
    [SerializeField] private Gun selectedGun;
    [SerializeField] private int changeStep = 1;
    
    private Rigidbody playerRigidbody;
    
    private Vector3 direction;
    private bool isGrounded;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isGrounded) return;
        
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }
        
        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeGun(changeStep);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ChangeGun(-changeStep);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 normalizedDirection = direction.normalized;
        
        playerRigidbody.linearVelocity = 
            new Vector3(normalizedDirection.x * speed, playerRigidbody.linearVelocity.y, normalizedDirection.z * speed);
    }
    
    public void ChangeSpeed(float speedAmount)
    {
        speed += speedAmount;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out TerrainCollider terrainCollider))
        {
            isGrounded = true;
        }
    }

    private void ChangeGun(int step)
    {
        int currentIndex = guns.IndexOf(selectedGun);
        int length = guns.Count;
        int index = (currentIndex + step  + length) % length;
            
        selectedGun.gameObject.SetActive(false);
        selectedGun = guns[index];
        selectedGun.gameObject.SetActive(true);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out TerrainCollider terrainCollider))
        {
            isGrounded = false;
            playerRigidbody.AddForce(Physics.gravity);
        }
    }
}