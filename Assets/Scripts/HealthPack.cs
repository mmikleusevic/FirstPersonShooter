using System;
using System.Collections;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private float healValue = 20;
    [SerializeField] private float duration = 1.5f;
    
    private Rigidbody rb;

    private void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self);
        
        float yPosition = Mathf.PingPong(Time.time, 0.5f);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.HealFromHealthPack(healValue, duration);
            Destroy(gameObject);
        }
    }
}
